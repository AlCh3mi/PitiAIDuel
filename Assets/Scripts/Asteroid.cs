using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Vector2 velocity;
    [SerializeField] private float angularVelocity;
    [SerializeField] private float size = 5f;
    
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        transform.localScale = Vector2.one * Random.Range(1, size);
        AddRandomAngularVelocity();
        AddRandomDirectionalForce();
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