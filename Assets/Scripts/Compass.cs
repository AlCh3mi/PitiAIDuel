using System;
using JetBrains.Annotations;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] [CanBeNull] private Transform target;
    [SerializeField] private Transform compass;
    
    public void SetTarget([CanBeNull] Transform pTarget)
    {
        target = pTarget;
    }

    private void Update()
    {
        var isActive = target != null;
        
        compass.gameObject.SetActive(isActive);
        
        if(!isActive)
            return;

        var toTarget = (target.position - transform.position);
        var rotation = Quaternion.LookRotation(Vector3.forward, toTarget);
        compass.transform.rotation = rotation;
    }
}