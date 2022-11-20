using UnityEngine;
using UnityEngine.Events;

namespace Spaceship
{
    public class CargoBay : MonoBehaviour
    {
        public UnityEvent<int> dustUpdated;
        
        private int asteroidDust;

        public int AsteroidDust
        {
            get => asteroidDust;
            private set
            {
                asteroidDust = value;
                dustUpdated?.Invoke(asteroidDust);
            }
        }

        public bool HasEnough(int amount) => amount >= AsteroidDust;

        public void Decrement(int amount)
        {
            if (!HasEnough(Mathf.Abs(amount)))
                return;

            AsteroidDust -= amount;
        }

        public void Increment(int amount)
        {
            if(amount <= 0)
                return;
            
            AsteroidDust += amount;
        }
        
        
        void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 100, 20), $"{AsteroidDust}");
        }
    }
}