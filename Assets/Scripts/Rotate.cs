using System;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Rotate(Vector3.forward * (speed * Time.deltaTime), Space.Self);
    }
}