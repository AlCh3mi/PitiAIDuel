using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectsSlider;

        private void OnEnable()
        {
            masterSlider.SetValueWithoutNotify(AudioManager.Instance.MasterVolume);
            musicSlider.SetValueWithoutNotify(AudioManager.Instance.MusicVolume);
            effectsSlider.SetValueWithoutNotify(AudioManager.Instance.EffectsVolume);
        }

        public void OnMasterVolumeChanged(float value) => AudioManager.Instance.SetMasterVolume(value);

        public void OnMusicVolumeChanged(float value) => AudioManager.Instance.SetMusicVolume(value);

        public void OnEffectsVolumeChanged(float value) => AudioManager.Instance.SetEffectsVolume(value);
    }
}