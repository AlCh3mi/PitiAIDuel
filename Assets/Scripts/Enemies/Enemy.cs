using System;
using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// This is lazy, implement State Machine - but first decide on exact enemy behaviour
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        public enum EnemyState
        {
            Search,
            Destroy
        }

        private IDamageable target;
        private EnemyState state;

        private void Start()
        {
            state = EnemyState.Search;
        }

        private void Update()
        {
            switch (state)
            {
                case EnemyState.Search:
                    
                    if (target != null)
                        state = EnemyState.Destroy;

                    break;
                case EnemyState.Destroy:
                    
                    if (target == null)
                        state = EnemyState.Search;
                    
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                if (col.TryGetComponent<IDamageable>(out var damageable))
                {
                    target = damageable;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent<IDamageable>(out var damageable)) 
                return;
            
            if (target == damageable)
                target = null;
        }
    }
}