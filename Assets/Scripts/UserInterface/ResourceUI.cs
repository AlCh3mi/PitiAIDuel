using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        public void UpdateText(int amount)
        {
            text.text = amount.ToString();
        }
    }
}