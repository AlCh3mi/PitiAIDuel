using System;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Loot
{
    public class OreCollectable : MonoBehaviour, ICollectableLoot
    {
        [SerializeField] private int min;
        [SerializeField] private int max;
        public int Amount { get; private set; }

        private void Start()
        {
            SetAmount(Random.Range(min, max));
        }

        private void SetAmount(int amount)
        {
            Amount = amount;
        }
        
        public void Collect(GameObject collector)
        {
            if(collector.TryGetComponent<IMiner>(out var miner))
            {
                miner.Increment(Amount);
                Destroy(gameObject);
            }
        }
    }
}