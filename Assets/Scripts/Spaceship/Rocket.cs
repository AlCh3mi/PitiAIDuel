using System;
using Interfaces;
using UnityEngine;

namespace Spaceship
{
    public class Rocket : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float explosionRadius;
        [SerializeField] private LayerMask affectedLayers;
        [SerializeField] private float damage;

        private Rigidbody2D rb2d;
        
        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            rb2d.AddForce(transform.up * speed, ForceMode2D.Force);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Detonate();
        }

        public void Detonate()
        {
            //Play Explode Audio
            //Disable spriteRenderer? or GameObject?
            //Play Explosion ParticleEffect, maybe unparent it?
            var hits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero, 0f, affectedLayers);

            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent<IDamageable>(out var damageable))
                {
                    //var calculatedDamage = hit.point
                    //Calculate distance from epicentre of explosion
                    //damage * distance/radius
                    damageable.TakeDamage(damage);
                    //add force from centre towards hit position
                }
            }
        }
    }
}