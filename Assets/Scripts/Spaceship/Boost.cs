using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Spaceship
{
    public class Boost : MonoBehaviour
    {
        [SerializeField] private float lightIntensity = 2.5f;
        [SerializeField] private ParticleSystem boostParticle;
        [SerializeField] private Light2D light2D;
        [SerializeField] private Locomotion locomotion;
        [SerializeField] private AudioSource boostAudioSource;
        [SerializeField] private float minPitch = 0.8f;
        [SerializeField] private float maxPitch = 1.5f;

        private float desiredMaxVolume;

        private void Awake()
        {
            desiredMaxVolume = boostAudioSource.volume;
        }

        private void Update()
        {
            if(!locomotion.IsBoosting)
            {
                boostParticle.Stop();
                light2D.intensity = Mathf.Lerp(light2D.intensity, 0f, Time.deltaTime);
                
                boostAudioSource.Pause();
                boostAudioSource.pitch = minPitch;
                boostAudioSource.volume = 0.1f;
                return;
            }

            boostParticle.Play();
            
            light2D.intensity = Mathf.Lerp(light2D.intensity, lightIntensity, Time.deltaTime);
            
            boostAudioSource.UnPause();
            boostAudioSource.volume = Mathf.Lerp(boostAudioSource.volume, desiredMaxVolume, Time.deltaTime);
            boostAudioSource.pitch = Mathf.Lerp(boostAudioSource.pitch, maxPitch, Time.deltaTime);
        }
    }
}