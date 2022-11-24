using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Spaceship
{
    public class Headlight : MonoBehaviour
    {
        [SerializeField] private float energyDrain;
        [SerializeField] private Light2D headLight;
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private Energy energy;
        
        private void Update()
        {
            if (inputHandler.HeadLight && energy.HasEnough(energyDrain))
            {
                headLight.enabled = true;
                energy.Drain(energyDrain);
                return;
            }

            headLight.enabled = false;
        }
    }
}