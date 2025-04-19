using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToFollow;
    private Vector3 objectOffset;
    private void Start()
    {
        objectOffset = objectToFollow.transform.position - transform.position;
    }
    
    private void LateUpdate()
    {
        transform.position = objectToFollow.transform.position - objectOffset;
    }
}
