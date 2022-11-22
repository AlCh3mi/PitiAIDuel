using System;
using Cinemachine;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private int worldSize;
    [Header("Dependencies")]
    [SerializeField] private Transform background;
    [SerializeField] private ParticleSystem starParticleSystem;
    [SerializeField] private Transform boundsParent;
    [SerializeField] private AsteroidSpawner asteroidSpawner;
    [Header("Camera")] 
    [SerializeField] private CinemachineConfiner2D confiner;
    [SerializeField] private PolygonCollider2D cameraConfiner;

    [ContextMenu("Generate World")]
    public void GenerateWorld()
    {
        var size = worldSize;
        SetBackgroundSize(size);
        SetStarSystemSize(size);
        AdjustBounds(size);
        SetCameraBounds(size);
        SpawnAsteroids(size / 4, size);
    }

    private void SetBackgroundSize(int size) => background.localScale = Vector2.one * size;

    private void SetStarSystemSize(int size)
    {
        var shape = starParticleSystem.shape;
        shape.scale = new Vector3(size, 1, size);
        
        var emission = starParticleSystem.emission;
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(size);
    }

    private void AdjustBounds(int size)
    {
        var bound = (size / 2);
        var top = boundsParent.GetChild(0);
        top.position = Vector2.up * bound;
        top.GetComponent<BoxCollider2D>().size = new Vector2(size, 0.1f);
        
        var bottom = boundsParent.GetChild(1);
        bottom.position = Vector2.down * bound;
        bottom.GetComponent<BoxCollider2D>().size = new Vector2(size, 0.1f);

        var left = boundsParent.GetChild(2);
        left.position = Vector2.left * bound;
        left.GetComponent<BoxCollider2D>().size = new Vector2(0.1f, size);
        
        var right = boundsParent.GetChild(3);
        right.position = Vector2.right * bound;
        right.GetComponent<BoxCollider2D>().size = new Vector2(0.1f, size);
    }

    private void SetCameraBounds(int size)
    {
        var bound = size / 2;
        cameraConfiner.points = new[]
        {
            new Vector2(-bound, bound),
            new Vector2(-bound, -bound),
            new Vector2(bound, -bound),
            new Vector2(bound, bound)
        };
        
        confiner.InvalidateCache();
    }

    private void SpawnAsteroids(int quantity, int radius)
    {
        if(asteroidSpawner.AsteroidParent.childCount > 0)
            asteroidSpawner.ClearAsteroids();
        
        asteroidSpawner.SpawnAsteroids(quantity, radius);
    }

    private float Area(int radius)
    {
        return MathF.PI * (Mathf.Pow(radius, 2));
    }

    private void foo(int size, int numObjects)
    {
        var blah = Area(size) / numObjects;
    }
}