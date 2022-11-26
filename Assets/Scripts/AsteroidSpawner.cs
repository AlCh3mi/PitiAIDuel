using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid prefab;
    [SerializeField] private float maxSize = 5f;
    [SerializeField] private LayerMask preventionLayerMask;
    [SerializeField] private Transform parent;
    [SerializeField] private int counterThreshold = 15;

    [Header("Manual Spawning")] 
    [SerializeField] private int amount;
    [SerializeField] private int rad;

    public Transform AsteroidParent => parent;
    
    public void SpawnAsteroids(int quantity, int radius)
    {
        var spawnedAsteroids = 0;
        var counter = 0;
        
        while (spawnedAsteroids <= quantity)
        {
            var position = GetRandomPositionInsideBounds(radius);
            
            if (!CanSpawnAtPosition(position))
            {
                if (counter > counterThreshold)
                {
                    Debug.LogWarning($"Unable to find valid position to place Asteroid. Spawned {spawnedAsteroids}/{quantity}");
                    break;
                }
                
                counter++;
                continue;
            }
            
            Instantiate(prefab, position, Quaternion.identity, parent);
            spawnedAsteroids++;
        }
    }
    
    [ContextMenu("Clear Asteroids")]
    public void ClearAsteroids()
    {
        for (var i = parent.childCount - 1; i >= 0; i--)
            DestroyImmediate(parent.GetChild(i).gameObject);
    }

    [ContextMenu("Spawn Asteroids")]
    public void ContextSpawnAsteroids()
    {
        SpawnAsteroids(amount, rad);
    }

    private Vector2 GetRandomPositionInsideBounds(int radius) => Random.insideUnitCircle * radius / 2;

    private bool CanSpawnAtPosition(Vector2 position)
    {
        var hit = Physics2D.CircleCast(position, maxSize, Vector2.zero, 0, preventionLayerMask);
        return hit.collider == null;
    }
}