using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Rotator : MonoBehaviour
{
    [SerializeField] private bool x;
    [SerializeField] private bool y;
    [SerializeField] private bool z;
    [SerializeField] private float speed;
    [SerializeField] private float startRotationMin = 1;
    [SerializeField] private float startRotationMax = 180;
    
    void Start()
    { 
        gameObject.transform.Rotate(
            Random.Range(startRotationMin, startRotationMax), 
            Random.Range(startRotationMin, startRotationMax), 
            Random.Range(startRotationMin, startRotationMax));
    }
    
    void Update()
    {
        gameObject.transform.Rotate(
            x ? speed * Time.deltaTime : 0, 
            y ? speed * Time.deltaTime : 0, 
            z ? speed * Time.deltaTime : 0);
    }
}
