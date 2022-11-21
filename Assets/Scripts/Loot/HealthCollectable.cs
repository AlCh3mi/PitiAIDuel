using UnityEngine;

namespace Loot
{
    public class HealthCollectable : MonoBehaviour, ICollectableLoot
    {
        [SerializeField] private int healthRestored;
        
        public void Collect(GameObject collector)
        {
            if(!collector.CompareTag("Player"))
                return;
            
            if (collector.TryGetComponent<Health>(out var health))
            {
                health.Heal(healthRestored);
                Destroy(gameObject);
            }
        }
    }
}