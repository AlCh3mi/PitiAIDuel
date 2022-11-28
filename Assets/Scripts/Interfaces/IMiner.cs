namespace Interfaces
{
    public interface IMiner
    {
        float Resource { get; }
        float Deposit();
        void Increment(float amount);
        void Decrement(float amount);
        bool HasEnough(float amount);
    }
}