using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Mothership
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private float cooldown;
        [SerializeField, Range(-1f, 1f)] private float turretAccuracy;
        [SerializeField] private GameObject projectile;
        [SerializeField] private Transform firePoint;
        [SerializeField] private TurretInitSystem initSystem;
        [Header("Lighting")]
        [SerializeField] private Light2D searchLight;
        [SerializeField] private Color neutralColor;
        [SerializeField] private Color hostileColor;
        [Header("Audio")] 
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip pewClip;
        

        private float lastFired;
        private Transform target;

        private bool CanFire => Time.time - lastFired >= cooldown;

        private void Fire()
        {
            if(!CanFire || !initSystem.IsInitialized)
                return;
            
            var direction = Quaternion.LookRotation(firePoint.forward, firePoint.up);
            Instantiate(projectile, firePoint.position, direction);
            audioSource.PlayOneShot(pewClip);
            lastFired = Time.time;
        }

        private void Update()
        {
            if(!initSystem.IsInitialized)
                return;
            
            if (target == null)
            {
                searchLight.color = neutralColor;
                transform.up = Vector2.Lerp(transform.up, Vector2.down, Time.deltaTime);
                return;
            }

            searchLight.color = hostileColor;
            var toTarget = (Vector2)(target.position - transform.position);
            transform.up = Vector2.Lerp(transform.up, toTarget, Time.deltaTime);

            if (Vector2.Dot(transform.up, toTarget.normalized) > turretAccuracy)
                Fire();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
                target = col.transform;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                target = null;
        }
    }
}