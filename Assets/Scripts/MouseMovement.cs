using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float shotPower=1;
    [SerializeField] private LineRenderer lineRenderer;
    private Vector3 screenPosition;
    private Vector3 worldPosition;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse.current.position.readValue()
        /*screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane + 1;
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = worldPosition;*/
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

    private Vector3? CastMouseClickRay()
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
    }
    
}
