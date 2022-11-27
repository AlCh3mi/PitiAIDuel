using System;
using UnityEngine;

namespace Spaceship
{
    public class RocketLauncher : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private Rocket prefab;
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private float cooldown;
        
        private float lastFired;

        public bool CanFire => Time.time - lastFired >= cooldown;
        
        private void Update()
        {
            if (inputHandler.SecondaryFire && CanFire)
                Launch();
        }

        private void Launch()
        {
            Instantiate(prefab, firePoint.position, firePoint.rotation);
            lastFired = Time.time;
        }
    }
}