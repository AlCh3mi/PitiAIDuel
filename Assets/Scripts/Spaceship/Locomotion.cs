using UnityEngine;

namespace Spaceship
{
    public class Locomotion : MonoBehaviour
    {
        [Header("Thruster")]
        [Space]
        [SerializeField] private float moveSpeed;
        [Header("Boost")]
        [Space]
        [SerializeField] private float boostSpeed;
        [SerializeField] private float boostDrain = 10f;
        [Header("Brake")]
        [Space]
        [SerializeField] private float brakeStrength;
        [Header("Dependencies")]
        [Space]
        [SerializeField] private Transform model;
        
        private Rigidbody2D rb2d;
        private InputHandler inputHandler;
        private Energy energy;

        public bool IsBoosting => inputHandler.Boost && energy.HasEnough(boostDrain);
    
        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            inputHandler = GetComponent<InputHandler>();
            energy = GetComponent<Energy>();
        }

        private void FixedUpdate()
        {
            if(inputHandler.Brake)
            {
                rb2d.velocity = Vector2.Lerp(rb2d.velocity, Vector2.zero, brakeStrength * Time.deltaTime);
                return;
            }
            
            if(inputHandler.Thruster)
                rb2d.AddForce(model.up * moveSpeed, ForceMode2D.Force);
        
            if(IsBoosting)
            {
                rb2d.AddForce(model.up * boostSpeed, ForceMode2D.Force);
                energy.Drain(boostDrain);
            }
        }
    }
}
