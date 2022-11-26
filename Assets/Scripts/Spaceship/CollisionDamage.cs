using System;
using UnityEngine;

namespace Spaceship
{
    public class CollisionDamage : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private float collisionDamageMultiplier = 2f;
        [SerializeField] private float maxCollisionDamage = 20f;
        [Tooltip("If relative velocity is greater than this, dmg will be taken")]
        [SerializeField] private float collisionThreshold;
    
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.layer != LayerMask.NameToLayer("Default")) 
                return;
            
            var impact = col.relativeVelocity;

            if(impact.magnitude < collisionThreshold)
                return;
                
            var dmg = impact.magnitude * collisionDamageMultiplier;
            dmg = Mathf.Clamp(dmg, 0, maxCollisionDamage);
            health.TakeDamage(dmg);
        }
    }
}