using Interfaces;
using UnityEngine;

namespace Loot
{
    public class EnergyCollectable : MonoBehaviour, ICollectableLoot
    {
        [SerializeField] private int restoreAmount = 5;
        public void Collect(GameObject collector)
        {
            Debug.Log($"{restoreAmount} Energy restored");
            Destroy(gameObject);
        }
    }
}