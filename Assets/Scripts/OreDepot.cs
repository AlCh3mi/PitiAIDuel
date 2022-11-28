using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class OreDepot : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip depositAudioClip;
    
    public UnityEvent<float> oreReservesUpdated;

    private float amount;
    public float Amount
    {
        get => amount;
        private set
        {
            amount = value;
            oreReservesUpdated?.Invoke(Amount);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<IMiner>(out var miner))
        {
            var deposit = miner.Deposit();
            
            if(deposit <= 0)
                return;
            
            audioSource.PlayOneShot(depositAudioClip);
            Amount += deposit;
        }
    }
    
    //todo Add Total Amount deposited user feedback (world space or UI)
}