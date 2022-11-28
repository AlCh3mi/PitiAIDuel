using Interfaces;
using UnityEngine;

public class SurvivalCraft : MonoBehaviour, IRepairable
{
    [field: SerializeField] public Health Health { get; private set; }
    
    public void Repair(float repairAmount)
    {
        if(Health.Current < Health.Max)
            Health.Heal(repairAmount * Time.deltaTime);    
    }
}