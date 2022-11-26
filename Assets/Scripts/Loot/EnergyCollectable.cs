using Interfaces;
using Spaceship;
using UnityEngine;

namespace Loot
{
    public class EnergyCollectable : MonoBehaviour, ICollectableLoot
    {
        [SerializeField] private int restoreAmount = 25;
        public void Collect(GameObject collector)
        {
            if (collector.TryGetComponent<Energy>(out var energy))
            {
                Debug.Log($"{restoreAmount} Energy restored");
                energy.Charge(restoreAmount);
                Destroy(gameObject);
            }
        }
    }
}