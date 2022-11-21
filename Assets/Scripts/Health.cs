using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float current;
    [SerializeField] private float max;
    [field:SerializeField] public float InvulnerabilityPeriodOnDamaged { get; private set; }
    
    [Space(20)]
    public UnityEvent damaged;
    public UnityEvent<float, float> healthChanged;
    public UnityEvent death;
    
    private float lastDamaged;
    
    public float Current
    {
        get => current;
        private set
        {
            current = Mathf.Clamp(value, 0, Max);
            healthChanged?.Invoke(current, Max);
        }
    }
    
    public float Max 
    { 
        get => max; 
        private set => max = Mathf.Clamp(value, 0, float.MaxValue); 
    }
    
    public bool IsDead => Current <= 0 ;

    private bool Vulnerable => Time.time - lastDamaged >= InvulnerabilityPeriodOnDamaged;
    
    private void Awake()
    {
        Current = Max;
        lastDamaged = Time.time - InvulnerabilityPeriodOnDamaged;
    }

    private void Start()
    {
        healthChanged?.Invoke(Current, Max);
    }

    public void SetMaxHealth(float newMax)
    {
        if (newMax <= 0)
            return;
        
        Max = newMax;
        
        if(Current > Max)
            Current = Max;
    }
    
    public void Heal(float healing)
    {
        if(healing <= 0)
            return;
        
        if(IsDead)
            return;

        Current += healing;
    }

    public void TakeDamage(float damage)
    {
        if(damage <= 0)
            return;
        
        if(!Vulnerable)
            return;
        
        if (IsDead)
            return;
        
        Current -= damage;
        damaged?.Invoke();
        
        if(IsDead)
            death?.Invoke();
    }

    //todo: remove
    [ContextMenu("Take Damage")] 
    public void DEV_TakeDamage() => TakeDamage(Random.Range(1f, Current));
}