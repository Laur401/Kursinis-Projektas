using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private float shotPower=1;
    [SerializeField] private LineRenderer lineRenderer;
    private Vector3 screenPosition;
    private Vector3 worldPosition;
    private Rigidbody rb;
    private LevelScoringManager lsm;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lsm = FindAnyObjectByType<LevelScoringManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3? worldPoint = CastMouseClickRay();
        if (!worldPoint.HasValue) return;
        DrawLine(worldPoint.Value);
        if (Input.GetMouseButtonDown(0))
            Shoot(worldPoint.Value);
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
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);
        
        rb.AddForce(direction * (strength * shotPower));
        lsm.AddStroke();
    }
    
    //TODO: Remove shooting while moving, add function that definitively stops the ball when movement is small enough.
    
}
