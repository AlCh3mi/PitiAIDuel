using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Spaceship
{
    public class CargoBay : MonoBehaviour, IMiner
    {
        public UnityEvent<float> dustUpdated;

        private float resource;

        public float Resource
        {
            get => resource;
            private set
            {
                resource = value;
                dustUpdated?.Invoke(resource);
            }
        }
        
        private void Start() => dustUpdated?.Invoke(resource);

        public bool HasEnough(float amount) => amount <= Resource;

        public void Decrement(float amount)
        {
            if (!HasEnough(Mathf.Abs(amount)))
                return;

            Resource -= amount;
        }

        public void Increment(float amount)
        {
            if(amount <= 0)
                return;
            
            Resource += amount;
        }

        public float Deposit()
        {
            var deposited = Resource;
            Decrement(Resource);
            return deposited;
        }
    }
}