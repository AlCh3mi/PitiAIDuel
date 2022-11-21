using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Mothership
{
    public class TurretInitSystem : MonoBehaviour
    {
        [SerializeField] private float bootTime = 2f;
        [Header("Lighting System")]
        [SerializeField] private Light2D searchLight;
        [SerializeField] private float lightIntensity;
        [Header("Audio")] 
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip bootUpSound;
        
        public bool IsInitialized { get; private set; } 

        [ContextMenu("Power Up")]
        public void PowerUp()
        {
            StartCoroutine(Boot());
        }

        public void PowerDown()
        {
            StopAllCoroutines();
            audioSource.Stop();
            searchLight.intensity = 0f;
            searchLight.enabled = false;

            IsInitialized = false;
        }

        private IEnumerator Boot()
        {
            var elapsed = 0f;
            
            audioSource.PlayOneShot(bootUpSound);
            
            searchLight.intensity = 0f;
            searchLight.enabled = true;
            
            while (elapsed <= bootTime)
            {
                searchLight.intensity = Mathf.Lerp(0f, lightIntensity, elapsed / bootTime);
                yield return null;
                elapsed += Time.deltaTime;
            }

            searchLight.intensity = lightIntensity;
            IsInitialized = true;
        }
    }
}