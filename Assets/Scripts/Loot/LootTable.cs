using UnityEngine;

namespace Loot
{
    [CreateAssetMenu(menuName = "Loot/Loot Table")]
    public class LootTable : ScriptableObject
    {
        [field:SerializeField] public GameObject[] Loot { get; private set; }

        public GameObject GetRandomObject()
        {
            var rnd = Random.Range(0, Loot.Length);
            return Loot[rnd];
        }
    }
}