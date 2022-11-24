using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class ImageFillUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text text;

        public void UpdateFillAmount(float current, float max)
        {
            if(text)
                text.text = $"{((int)current).ToString()} / {((int)max).ToString()}";
            
            image.fillAmount = current / max;
        }
    }
}