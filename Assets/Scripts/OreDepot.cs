using System;
using UnityEngine;
using UnityEngine.Events;

public class OreDepot : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip depositAudioClip;
    
    public UnityEvent<int> oreReservesUpdated;

    private int amount;
    public int Amount
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
            audioSource.PlayOneShot(depositAudioClip);
            Amount += miner.Deposit();
        }
    }

    private void OnDrawGizmosSelected()
    {
        GUI.Label(new Rect(10, 10, 100, 20), Amount.ToString());
    }
}