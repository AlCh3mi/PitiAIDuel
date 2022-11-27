using UnityEngine;

namespace Audio
{
    public class OpenAudioMenu : MonoBehaviour
    {
        [SerializeField] private GameObject audioSettingsMenu;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                audioSettingsMenu.SetActive(!audioSettingsMenu.activeSelf);               
            }
        }
    }
}