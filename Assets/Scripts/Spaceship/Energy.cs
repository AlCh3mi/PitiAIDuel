using UnityEngine;
using UnityEngine.Events;

namespace Spaceship
{
    public class Energy : MonoBehaviour
    {
        [field: SerializeField] public float Max { get; private set; } = 1000;
        [SerializeField] private float rechargeDelay = 2f;
        [SerializeField] private float rechargeRate = 5f;

        [SerializeField] private UnityEvent<float, float> updatedEvent;
        
        private float lastUsed;
        private float _current;
        
        public float Current
        {
            get => _current;
            private set
            {
                _current = Mathf.Clamp(value, 0f, Max);
                updatedEvent?.Invoke(_current,Max);
            }
        }
        
        private bool ShouldRecharge => Time.time - lastUsed >= rechargeDelay;

        public bool HasEnough(float amount) => amount <= Current;

        private void Update()
        {
            if(!ShouldRecharge || Current >= Max)
                return;

            if(Current < Max)
                Current += rechargeRate * Time.deltaTime;
        }

        private void Start()
        {
            Current = Max;
            lastUsed = -rechargeDelay;
        }
        
        public void Drain(float drain)
        {
            Current -= drain * Time.deltaTime;
            lastUsed = Time.time;
        }

        public void Charge(float charge)
        {
            Current += Mathf.Abs(charge);
        }
    }
}