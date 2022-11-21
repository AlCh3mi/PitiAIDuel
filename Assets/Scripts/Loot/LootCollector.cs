using System;
using UnityEngine;

namespace Loot
{
    public class LootCollector : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!col.TryGetComponent<ICollectableLoot>(out var collectable))
                return;
        
            collectable.Collect(gameObject);
        }
    }
}