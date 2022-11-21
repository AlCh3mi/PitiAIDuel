using System;
using UnityEngine;

namespace Spaceship
{
    public class CollisionDamage : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private float collisionDamageMultiplier = 5f;
        [Tooltip("If impact magnitude is greater than this, dmg will be taken")]
        [SerializeField] private float collisionThreshold;
    
        private Rigidbody2D rb2d;

        private void Awake() => rb2d = GetComponent<Rigidbody2D>();

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                var impact = col.gameObject.TryGetComponent<Rigidbody2D>(out var rbody)
                    ? Vector2.Max(rb2d.velocity, rbody.velocity)
                    : rb2d.velocity;
                
                if(impact.magnitude < collisionThreshold)
                    return;
                
                var dmg = impact.magnitude * collisionDamageMultiplier;
                Debug.Log($"Velocity : {impact} - Magnitude: {impact.magnitude} - Damage: {dmg}");
                health.TakeDamage(dmg);
            }
        }
    }
}