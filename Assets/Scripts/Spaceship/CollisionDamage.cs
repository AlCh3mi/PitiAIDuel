using System;
using UnityEngine;

namespace Spaceship
{
    public class CollisionDamage : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private float collisionDamageMultiplier = 2f;
        [Tooltip("If impact magnitude is greater than this, dmg will be taken")]
        [SerializeField] private float collisionThreshold;
    
        private Rigidbody2D rb2d;

        private void Awake() => rb2d = GetComponent<Rigidbody2D>();

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                var impact = col.relativeVelocity;

                if(impact.magnitude < collisionThreshold)
                    return;
                
                var dmg = impact.magnitude * collisionDamageMultiplier;
                dmg = Mathf.Clamp(dmg, 0, 50);
                health.TakeDamage(dmg);
            }
        }
    }
}