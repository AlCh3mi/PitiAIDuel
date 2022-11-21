using System;
using UnityEngine;

namespace Spaceship
{
    public class Beam : MonoBehaviour
    {
        [SerializeField] private LayerMask layers;
        [SerializeField] private float maxDistance;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform firePoint;
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private ParticleSystem hitImpact;
        [SerializeField] private CargoBay cargoBay;
        
        private void Start()
        {
            Physics2D.queriesHitTriggers = false;
            hitImpact.Stop();
        }
        
        private void Update()
        {
            lineRenderer.enabled = inputHandler.PrimaryFire;
            
            if (inputHandler.PrimaryFire)
                Fire();
            else
            {
                hitImpact.Stop();
            }
        }

        public void Fire()
        {
            var hit = Physics2D.Raycast(firePoint.position, firePoint.up, maxDistance, layers);
            
            if (!hit)
            {
                hitImpact.Stop();
                lineRenderer.SetPositions(new []
                {
                    firePoint.position,
                    firePoint.position + (firePoint.up * maxDistance)
                });
                return;
            }

            hitImpact.transform.position = hit.point;
            hitImpact.transform.up = hit.normal;
            hitImpact.Play();
            
            lineRenderer.SetPositions(new []
            {
                firePoint.position,
                (Vector3)hit.point
            });

            if (hit.collider.TryGetComponent<IResource>(out var resource))
            {
                cargoBay.Increment(resource.Mine());
            }
        }
    }
}