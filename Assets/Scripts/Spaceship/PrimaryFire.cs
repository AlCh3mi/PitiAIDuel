using UnityEngine;

namespace Spaceship
{
    public class PrimaryFire : MonoBehaviour
    {
        [SerializeField] private float cooldown;
    
        [SerializeField] private GameObject projectile;
        [SerializeField] private Transform firePoint;

        [SerializeField] private InputHandler inputHandler;

        private float lastFired;

        private bool CanFire => Time.time - lastFired >= cooldown;

        private void Update()
        {
            if (CanFire && inputHandler.PrimaryFire)
                Fire();
        }

        private void Fire()
        {
            var direction = Quaternion.LookRotation(firePoint.forward, firePoint.up);
            Instantiate(projectile, firePoint.position, direction);
            lastFired = Time.time;
        }
    }
}