using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Spaceship
{
    public class Headlight : MonoBehaviour
    {
        [SerializeField] private Light2D headLight;
        [SerializeField] private InputHandler inputHandler;

        private void Update()
        {
            HeadLightEnabled(inputHandler.HeadLight);
        }

        public void HeadLightEnabled(bool enabled)
        {
            headLight.enabled = enabled;
        }
    }
}