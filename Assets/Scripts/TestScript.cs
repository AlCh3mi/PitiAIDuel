using System;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private Vector2 initialVelocityA;
    [SerializeField] private Vector2 initialVelocityB;

    [SerializeField] private GameObject A;
    [SerializeField] private GameObject B;

    private Rigidbody2D rbA;
    private Rigidbody2D rbB;

    private Vector2 VBA;
    private void Awake()
    {
        rbA = A.GetComponent<Rigidbody2D>();
        rbB = B.GetComponent<Rigidbody2D>();

        rbA.gravityScale = 0f;
        rbB.gravityScale = 0f;
    }

    private void Start()
    {
        rbA.velocity = initialVelocityA;
        rbB.velocity = initialVelocityB;
    }

    private void Update()
    {
        VBA = rbB.velocity - rbA.velocity;
        //Debug.Log(rbB.position - rbA.position);
        Debug.Log(VBA);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), rbA.velocity.ToString());
        GUI.Label(new Rect(10, 30, 100, 20), rbB.velocity.ToString());
        GUI.Label(new Rect(10, 50, 100, 20), "A Relative to B : " +VBA);
    }
}