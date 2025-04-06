using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutOfLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = other.GetComponent<MouseMovement>().shootLocations[^1];
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
