﻿using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Spaceship
{
    public class Boost : MonoBehaviour
    {
        [SerializeField] private float lightIntensity = 2.5f;
        [SerializeField] private ParticleSystem thrusterParticle;
        [SerializeField] private Light2D light2D;
    
        private InputHandler inputHandler;

        private void Awake()
        {
            inputHandler = GetComponent<InputHandler>();
        }

        private void Update()
        {
            if(!inputHandler.Boost)
            {
                thrusterParticle.Stop();
                light2D.intensity = Mathf.Lerp(light2D.intensity, 0f, Time.deltaTime);
                return;
            }

            thrusterParticle.Play();
            light2D.intensity = Mathf.Lerp(light2D.intensity, lightIntensity, Time.deltaTime);
        }
    }
}