namespace Interfaces
{
    public interface IMiner
    {
        int Resource { get; }
        int Deposit();
        void Increment(int amount);
        void Decrement(int amount);
        bool HasEnough(int amount);
    }
}