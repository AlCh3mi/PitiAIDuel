using UnityEngine;

namespace Loot
{
    public interface ICollectableLoot
    {
        void Collect(GameObject collector);
    }
}