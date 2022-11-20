public interface IResource
{
    int Reserve { get; }
    int Chunk { get; }
    
    float MineDuration { get; }

    int Mine();
}