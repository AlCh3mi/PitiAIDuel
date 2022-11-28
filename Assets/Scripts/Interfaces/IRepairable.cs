namespace Interfaces
{
    public interface IRepairable
    {
        public Health Health { get; }
        void Repair(float repairAmount);
    }
}