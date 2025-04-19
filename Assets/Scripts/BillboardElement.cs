using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardElement : MonoBehaviour
{
    private void LateUpdate()
    {
        if (Camera.main == null) return;
        transform.LookAt(Camera.main.transform.position, Vector3.up);
        //transform.forward = Camera.main.transform.forward;
    }
}
