using UnityEngine;

namespace Loot
{
    public class LoreCollectable : MonoBehaviour, ICollectableLoot
    {
        [SerializeField] private int storyIndex; //todo probably replace this with scriptableObject containing story Data
        public void Collect(GameObject collector)
        {
            PlayerPrefs.SetInt($"Story{storyIndex}", 1);
            Destroy(gameObject);
        }
    }
}