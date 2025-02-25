using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    void Start()
    {
        //Application.targetFrameRate = 60;
    }
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        //Rotate - is didz., nurodo, kad tai yra metodas

        //Debug.log(1.0f / Time.deltaTime);
    }
}
