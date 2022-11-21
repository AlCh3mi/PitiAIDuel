using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid prefab;
    [SerializeField] private float radius = 45;
    [SerializeField] private int quantity = 50;
    [SerializeField] private float maxSize = 5f;
    [SerializeField] private LayerMask preventionLayerMask;
    [SerializeField] private Transform parent;


    private void Start() => SpawnAsteroids();

    [ContextMenu("Spawn Asteroids")]
    public void SpawnAsteroids()
    {
        var spawnedAsteroids = 0;
        
        while (spawnedAsteroids <= quantity)
        {
            var position = GetRandomPositionInsideBounds();
            
            if (!CanSpawnAtPosition(position)) 
                continue;
            
            Instantiate(prefab, position, Quaternion.identity, parent);
            spawnedAsteroids++;
        }
    }

    private Vector2 GetRandomPositionInsideBounds() => Random.insideUnitCircle * radius;

    private bool CanSpawnAtPosition(Vector2 position)
    {
        var hit = Physics2D.CircleCast(position, maxSize, Vector2.zero, 0, preventionLayerMask);
        return hit.collider == null;
    }
}