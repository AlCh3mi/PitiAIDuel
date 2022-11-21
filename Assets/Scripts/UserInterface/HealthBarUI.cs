using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text text;

        public void UpdateHealthBar(float health, float max)
        {
            text.text = $"{((int)health).ToString()} / {((int)max).ToString()}";
            image.fillAmount = health / max;
        }
    }
}