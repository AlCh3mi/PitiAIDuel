using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Loot
{
    public class Loot : MonoBehaviour
    {
        [SerializeField] private LootTable lootTable;
        [SerializeField] private int minDrops = 1;
        [SerializeField] private int maxDrops = 5;
    
        public void Drop()
        {
            int amount = Random.Range(minDrops, maxDrops + 1);
            for (int i = 0; i < amount; i++)
            {
                Instantiate(lootTable.GetRandomObject(), (Vector2)transform.position + Random.insideUnitCircle, quaternion.identity);    
            }
        }

        private void OnValidate()
        {
            if (minDrops > maxDrops)
                maxDrops = minDrops;
        }
    }
}