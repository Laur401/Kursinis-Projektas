using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Rendering.PostProcessing;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private float shotPower=1;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float stopVelocity;
    private Vector3 screenPosition;
    private Vector3 worldPosition;
    private Rigidbody rb;
    private LevelScoringManager lsm;

    private bool isMoving = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lsm = FindAnyObjectByType<LevelScoringManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            Vector3? worldPoint = CastMouseClickRay();
            if (!worldPoint.HasValue) return;
            worldPoint = ClampMousePoint(worldPoint.Value);
            
            DrawLine(worldPoint.Value);
        
            if (Input.GetMouseButtonDown(0))
                Shoot(worldPoint.Value);
        }
        
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
        lineRenderer.enabled = true;
    }

    private static Vector3? CastMouseClickRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        if (Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, float.PositiveInfinity))
        {
            return hit.point;
        }
        else return null;
    }

    private void Shoot(Vector3 worldPoint)
    {
        Vector3 horizontalWorldPoint = worldPoint;
        horizontalWorldPoint.y = transform.position.y;

        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float strength = Mathf.Clamp(Vector3.Distance(transform.position, horizontalWorldPoint), 0f, 10f);

        direction = AdjustVelocityDirectionToSlope(direction);
        
        rb.AddForce(direction * (strength * shotPower));
        
        lsm.AddStroke();
    }

    private Vector3 ClampMousePoint(Vector3 worldPoint)
    {
        return Vector3.MoveTowards(gameObject.transform.position, worldPoint, 10f);
        /*if (Vector3.Distance(gameObject.transform.position, worldPoint) > 30f)
        {
            return Vector3.MoveTowards(gameObject.transform.position, worldPoint, 30f);
        }
        else return worldPoint;*/
    }
    
    private void CheckMovement() //TODO: Figure out how to optimize this to not need to be called every frame.
    {
        if (rb.velocity.sqrMagnitude < stopVelocity)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            lineRenderer.enabled = true;
            isMoving = false;
        }
        else
        {
            lineRenderer.enabled = false;
            isMoving = true;
        }
    }

    private Vector3 AdjustVelocityDirectionToSlope(Vector3 velocity)
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.2f))
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var adjustedVelocity = slopeRotation * velocity;
            return adjustedVelocity;
        }

        return velocity;
    }

}
