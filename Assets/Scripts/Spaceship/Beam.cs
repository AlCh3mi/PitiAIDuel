using System;
using Interfaces;
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
        [SerializeField] private int repairCost = 5;
        [SerializeField] private float repairAmount = 5f;
        [SerializeField] private CargoBay cargoBay;
        [SerializeField] private AudioSource beamImpactAudio;
        [SerializeField] private AudioSource beamAudio;
        
        private void Start()
        {
            Physics2D.queriesHitTriggers = false;
            hitImpact.Stop();
            beamImpactAudio.Pause();
            beamAudio.Pause();
            
        }
        
        private void Update()
        {
            lineRenderer.enabled = inputHandler.PrimaryFire;
            
            if (inputHandler.PrimaryFire)
                Fire();
            else
            {
                hitImpact.Stop();
                beamAudio.Pause();
                beamImpactAudio.Pause();
            }
        }

        public void Fire()
        {
            var hit = Physics2D.Raycast(firePoint.position, firePoint.up, maxDistance, layers);
            beamAudio.UnPause();
            if (!hit)
            {
                hitImpact.Stop();
                beamImpactAudio.Pause();
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
            beamImpactAudio.UnPause();
            
            lineRenderer.SetPositions(new []
            {
                firePoint.position,
                (Vector3)hit.point
            });

            if (hit.collider.TryGetComponent<IResource>(out var resource))
            {
                cargoBay.Increment(resource.Mine());
            }

            if (hit.collider.TryGetComponent<IRepairable>(out var repairable))
            {
                Debug.Log("Repairable Detected");
                if(cargoBay.HasEnough(repairCost))
                {
                    cargoBay.Decrement(repairCost * Time.deltaTime);
                    repairable.Repair(repairAmount);
                }
            }
        }
    }
}