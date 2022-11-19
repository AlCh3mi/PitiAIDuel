using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    [SerializeField] private float maxSpeed;
    
    private Vector3 smoothDampRef;
    private void Update()
    {
        var destination = target.position;
        destination.z = -10;
        
        transform.position =
            Vector3.SmoothDamp(transform.position, destination, ref smoothDampRef, smoothTime, maxSpeed);
    }
}