using System;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnter2d;
    [SerializeField] private UnityEvent onTriggerExit2d;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            onTriggerEnter2d?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onTriggerExit2d?.Invoke();        
        }
    }
}