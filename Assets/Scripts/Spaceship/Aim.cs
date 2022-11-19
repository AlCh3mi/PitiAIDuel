using UnityEngine;

namespace Spaceship
{
    public class Aim : MonoBehaviour
    {
        [SerializeField] private Transform model;

        private InputHandler inputHandler;

        private void Awake()
        {
            inputHandler = GetComponent<InputHandler>();
        }

        private void Update()
        {
            var between = inputHandler.MousePosition - (Vector2)model.position;
            model.up = between.normalized;
        }
    }
}