using System;
using UnityEngine;

namespace Spaceship
{
    public class Locomotion : MonoBehaviour
    {
        [SerializeField] private Transform model;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float brakeStrength;

        private Rigidbody2D rb2d;
        private InputHandler inputHandler;

    
        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            inputHandler = GetComponent<InputHandler>();
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
        }
    }
}
