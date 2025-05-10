using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Rendering.PostProcessing;

public class MouseMovement : MonoBehaviour, IGameEventMessageTarget
{
    [SerializeField] private float shotPower = 1;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float stopVelocity;
    [SerializeField] private float maxShootDistance = 10f;
    private Vector3 screenPosition;
    private Vector3 worldPosition;
    private Rigidbody rb;
    private LevelScoringManager lsm;
    [NonSerialized] public List<Vector3> shootLocations = new(); // Used by OutOfLevel.cs
    [SerializeField] private float timerLength = 3;
    private float timer;

    private bool isMoving = false;
    [SerializeField] private bool isDisabled = false;
    
    private UIInfoElements _uiElements;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lsm = FindAnyObjectByType<LevelScoringManager>();
        _uiElements = FindAnyObjectByType<UIInfoElements>();
        timer = timerLength;
        shootLocations.Add(transform.position);
        ExecuteEvents.Execute<IGameEventMessageTarget>(rb.gameObject, null,
            (handler, data) => handler.OnBallLocationUpdate(transform.position));
    }

    void ShootInput()
    {
        if (isMoving || isDisabled) return;
        if (!worldPoint.HasValue) return;
        Shoot(worldPoint.Value);
    }

    private Vector3? worldPoint = new();
    // Update is called once per frame
    void Update()
    {
        if (isMoving || isDisabled)
        {
            lineRenderer.enabled = false;
            _uiElements.SetStrength(0f);
            return;
        }
        lineRenderer.enabled = true;
        
        worldPoint = MouseReader.CastMouseRay();
        
        
        
        if (!worldPoint.HasValue) return;
        _uiElements.SetStrength(GetStrengthNormalized(worldPoint.Value));
        worldPoint = ClampMousePoint(worldPoint.Value);
        DrawLine(worldPoint.Value);

    }
    private void FixedUpdate()
    {
        CheckMovement();
    }

    private void DrawLine(Vector3 worldPoint)
    {
        Vector3[] positions =
        {
            transform.position,
            worldPoint,
        };
        lineRenderer.SetPositions(positions);
    }

    private void Shoot(Vector3 worldPoint)
    {
        Vector3 horizontalWorldPoint = worldPoint;
        horizontalWorldPoint.y = transform.position.y;
        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float strength = Mathf.Clamp(Vector3.Distance(transform.position, horizontalWorldPoint), 0f, maxShootDistance);

        direction = AdjustVelocityDirectionToSlope(direction);
        
        shootLocations.Add(transform.position);
        ExecuteEvents.Execute<IGameEventMessageTarget>(rb.gameObject, null,
            (handler, data) => handler.OnBallLocationUpdate(shootLocations[^1]));
        ExecuteEvents.Execute<IGameEventMessageTarget>(rb.gameObject, null,
            (handler, data) => handler.OnBallHit());
        
        rb.AddForce(direction * (strength * shotPower));
        lsm.AddStroke();
    }

    private float GetStrengthNormalized(Vector3 worldPoint)
    {
        Vector3 horizontalWorldPoint = worldPoint;
        horizontalWorldPoint.y = transform.position.y;
        return Mathf.Clamp(Vector3.Distance(transform.position, horizontalWorldPoint), 0f, maxShootDistance)/maxShootDistance;
    }

    private Vector3 ClampMousePoint(Vector3 worldPoint)
    {
        return Vector3.MoveTowards(gameObject.transform.position, worldPoint, maxShootDistance);
    }
    
    private void CheckMovement()
    {
        if (rb.velocity.sqrMagnitude < stopVelocity)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && isMoving)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                isMoving = false;
            }
        }
        else
        {
            timer = timerLength;
            isMoving = true;
        }
    }

    private Vector3 AdjustVelocityDirectionToSlope(Vector3 velocity)
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.2f))
        {
            //Debug.DrawRay(hitInfo.point, hitInfo.normal, Color.green, 5f);
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var adjustedVelocity = slopeRotation * velocity;
            //Debug.DrawRay(transform.position, adjustedVelocity*10, Color.red, 5f);
            return adjustedVelocity;
        }

        return velocity;
    }

    public void OnMousePowerupEnable() => isDisabled = true;
    public void OnMousePowerupDisable() => isDisabled = false;

    public void OnBallHitInput() => ShootInput();
}
