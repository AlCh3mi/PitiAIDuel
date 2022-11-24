using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Default Sound Settings")]
        [Tooltip("It will retrieve players audio settings from PlayerPrefs at startup, if not the defaults will be applied")]
        [SerializeField] private bool rememberPlayerAudioSettings;
        [SerializeField, Range(0.001f, 1f)] private float defaultMasterVol = 1f;
        [SerializeField, Range(0.001f, 1f)] private float defaultMusicVol = 1f;
        [SerializeField, Range(0.001f, 1f)] private float defaultEffectsVol = 1f;

        [Header("Dependencies"), Space]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioMixer mixer;

        public static AudioManager Instance { get; private set; }

        private const string MASTERVOLUME = "MasterVol";
        private const string MUSICVOLUME = "MusicVol";
        private const string EFFECTSVOLUME = "EffectsVol";
    
        public float MasterVolume => PlayerPrefs.GetFloat(MASTERVOLUME, defaultMasterVol);

        public float MusicVolume => PlayerPrefs.GetFloat(MUSICVOLUME, defaultMusicVol);

        public float EffectsVolume => PlayerPrefs.GetFloat(EFFECTSVOLUME, defaultEffectsVol);

        public AudioClip CurrentClip => musicSource.clip;
    

        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            if(musicSource.isPlaying)
                musicSource.Stop();

            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }

        public void SetMasterVolume(float value) => SetFloat(MASTERVOLUME, value);

        public void SetMusicVolume(float value) => SetFloat(MUSICVOLUME, value);

        public void SetEffectsVolume(float value) => SetFloat(EFFECTSVOLUME, value);

        private void SetFloat(string paramName, float value)
        {
            var logVol = Mathf.Log10(value) * 20;
            mixer.SetFloat(paramName, logVol);
            Debug.Log($"Setting {paramName} to : {value}");
            PlayerPrefs.SetFloat(paramName, value);
        }

        #region MonobehaviourCallbacks
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }
            
            DestroyImmediate(gameObject);
        }

        private void Start()
        {
            musicSource ??= GetComponent<AudioSource>();
            SetMixerVolumeValues();
        }

        private void SetMixerVolumeValues()
        {
            var str = rememberPlayerAudioSettings
                ? "Restoring Audio Settings"
                : "Default Audio Settings";
            Debug.Log(str);
            
            SetMasterVolume(rememberPlayerAudioSettings ? MasterVolume : defaultMasterVol);
            Debug.Log($"MASTER SAVED VALUE: " +MasterVolume);
            SetMusicVolume(rememberPlayerAudioSettings ? MusicVolume : defaultMusicVol);
            SetEffectsVolume(rememberPlayerAudioSettings ? EffectsVolume : defaultEffectsVol);
        }

        #endregion
    }
}
