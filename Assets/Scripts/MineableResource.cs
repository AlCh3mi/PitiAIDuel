using System;
using UnityEngine;

public class MineableResource : MonoBehaviour, IResource
{
    [field: SerializeField] public int Chunk { get; private set; } = 20;

    [field: SerializeField] public float MineDuration { get; private set; } = 2f;

    public int Reserve { get; private set; }
    
    private float resourceMineProgress;
    
    public int Mine()
    {
        if (Reserve <= 0)
            Destroy(gameObject);
        
        resourceMineProgress += Time.deltaTime;

        if (resourceMineProgress < MineDuration)
        {
            return 0;
        }
        
        if(Reserve < Chunk)
        {
            var remaining = Reserve;
            Reserve -= remaining;
            resourceMineProgress = 0f;
            return remaining;
        }
        
        Reserve -= Chunk;
        resourceMineProgress = 0f;
        
        if(Reserve <= 0)
            Destroy(gameObject);
        
        return Chunk;
    }

    public void SetResourceAmount(int amount)
    {
        Reserve = amount;
    }
}