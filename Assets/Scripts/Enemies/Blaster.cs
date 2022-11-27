using System;
using UnityEngine;

namespace Enemies
{
    public class Blaster : MonoBehaviour
    {
        [SerializeField] private float cooldown;
        [SerializeField] private Projectile prefab;
        [SerializeField] private Transform firePoint;

        private float lastFired;

        public bool CanFire => Time.time - lastFired >= cooldown;

        private void Start()
        {
            lastFired = -cooldown;
        }

        public void Fire()
        {
            if(!CanFire)
                return;

            Instantiate(prefab, firePoint.position, transform.rotation);
            lastFired = Time.time;
        }
    }
}