using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private KeyCode thruster = KeyCode.LeftShift;
    [SerializeField] private KeyCode thrusterAlt = KeyCode.W;
    [SerializeField] private KeyCode brake = KeyCode.Space;
    [SerializeField] private KeyCode brakeAlt = KeyCode.S;
    [SerializeField] private KeyCode primaryFire = KeyCode.Mouse0;
    [SerializeField] private KeyCode headLight = KeyCode.Mouse1;

    public Vector2 Input { get; private set; }
    public Vector2 MousePosition => (Vector2)mousePos;
    public bool Thruster => (UnityEngine.Input.GetKey(thruster) || UnityEngine.Input.GetKey(thrusterAlt)) && enabled;
    public bool Brake => (UnityEngine.Input.GetKey(brake) || UnityEngine.Input.GetKey(brakeAlt)) && enabled;

    public bool PrimaryFire => UnityEngine.Input.GetKey(primaryFire) && enabled;

    public bool HeadLight => UnityEngine.Input.GetKey(headLight) && enabled; 
    
    private Camera mainCam;
    private Vector3 mousePos;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        //Input = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
        mousePos = mainCam.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
    }
}