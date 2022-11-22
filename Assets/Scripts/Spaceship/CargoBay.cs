using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Spaceship
{
    public class CargoBay : MonoBehaviour, IMiner
    {
        public UnityEvent<int> dustUpdated;

        private int resource;

        public int Resource
        {
            get => resource;
            private set
            {
                resource = value;
                dustUpdated?.Invoke(resource);
            }
        }
        
        private void Start() => dustUpdated?.Invoke(resource);

        public bool HasEnough(int amount) => amount >= Resource;

        public void Decrement(int amount)
        {
            if (!HasEnough(Mathf.Abs(amount)))
                return;

            Resource -= amount;
        }

        public void Increment(int amount)
        {
            if(amount <= 0)
                return;
            
            Resource += amount;
        }

        public int Deposit()
        {
            var deposited = Resource;
            Decrement(Resource);
            return deposited;
        }
    }
}