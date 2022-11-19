using UnityEngine;

public class MineableResource : MonoBehaviour, IResource
{
    [field: SerializeField] public int Reserve { get; private set; } = 500;
    [field: SerializeField] public int Chunk { get; private set; } = 20;

    public int Mine()
    {
        if (Reserve <= 0)
            return 0;
        
        if(Reserve < Chunk)
        {
            var remaining = Reserve;
            Reserve -= remaining;
            return remaining;
        }
        
        Reserve -= Chunk;
        return Chunk;
    }
}

public interface IResource
{
    int Reserve { get; }
    int Chunk { get; }

    int Mine();
}

