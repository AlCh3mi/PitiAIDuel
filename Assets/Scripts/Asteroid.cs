using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Vector2 velocity;
    [SerializeField] private float angularVelocity;
    [SerializeField] private float size = 5f;
    [SerializeField] private MineableResource resource;

    public MineableResource Resource => resource;

    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        UpdateSize();
    }

    private void Start()
    {
        AddRandomAngularVelocity();
        AddRandomDirectionalForce();
        resource.SetResourceAmount((int)(rb2d.mass * 10));
    }

    private void UpdateSize()
    {
        transform.localScale = Vector2.one * Random.Range(1, size);
    }

    private void AddRandomDirectionalForce()
    {
        var direction = new Vector2()
        {
            x = Random.Range(-velocity.x, velocity.x),
            y = Random.Range(-velocity.y, velocity.y)
        };
        
        rb2d.AddForce(direction, ForceMode2D.Impulse);
    }

    private void AddRandomAngularVelocity()
    {
        var random = Random.Range(-angularVelocity, angularVelocity);
        rb2d.angularVelocity = random;
    }
}