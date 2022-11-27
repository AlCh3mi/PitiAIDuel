using System;
using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// This is lazy, implement State Machine - but first decide on exact enemy behaviour
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 0.1f;
        [SerializeField] private float turnSpeed = 2f;
        [SerializeField] private float aimStrictness = 0.8f;
        [SerializeField] private float proximityToLastKnownPos = 1f;
        [SerializeField] private Blaster blaster;

        private Rigidbody2D rb2d;
        private Transform target;
        private EnemyState state;
        private Vector3 lastKnownPosition;
        private bool hasEncounteredPlayer;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            state = EnemyState.Idle;
        }

        private void Update()
        {
            switch (state)
            {
                case EnemyState.Idle:
                    if (target != null)
                    {
                        state = EnemyState.Destroy;
                        return;
                    }

                    if (hasEncounteredPlayer)
                        state = EnemyState.Search;
                    
                    break;
                
                case EnemyState.Search:
                    if (target != null)
                    {
                        state = EnemyState.Destroy;
                        return;
                    }

                    if (!hasEncounteredPlayer)
                    {
                        state = EnemyState.Idle;
                        return;
                    }

                    var toLastKnownPos = lastKnownPosition - transform.position;
                    var desiredRot = Quaternion.LookRotation(transform.forward, toLastKnownPos);
                    transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, Time.deltaTime * turnSpeed);
                   
                    if(Vector2.Dot(transform.up, toLastKnownPos.normalized) >= 0.9f)
                        rb2d.AddForce(transform.up * moveSpeed, ForceMode2D.Force);
                    
                    if (Vector2.Distance(transform.position, lastKnownPosition) <= proximityToLastKnownPos)
                    {
                        rb2d.velocity = Vector2.zero;
                        state = EnemyState.Idle;
                    }
                    
                    break;
                case EnemyState.Destroy:
                    
                    if (target == null)
                    {
                        state = EnemyState.Idle;
                        return;
                    }
                    
                    var targetPos = target.position;
                    var toTarget = targetPos - transform.position;
                    var desiredRotation = Quaternion.LookRotation(transform.forward, toTarget);
                    transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime);

                    if (Vector2.Dot(transform.up, toTarget.normalized) >= aimStrictness)
                        blaster.Fire();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) 
                return;
            
            target = col.transform;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            target = null;
            lastKnownPosition = other.transform.position;
            hasEncounteredPlayer = true;
        }
        
        public enum EnemyState
        {
            Idle,
            Search,
            Destroy
        }

    }
}