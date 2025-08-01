using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int MaxHitCount { get; private set; } = 3;
    public int HitCount { get; private set; }
    public bool IsDead { get; private set; } = false;

    public event Action<int> OnHealthChange;
    public event Action OnDie;

    private void Awake()
    {
        HitCount = MaxHitCount;
    }

    public void RemoveHealth(int amount) 
    {
        HitCount -= amount;
        HitCount = Mathf.Clamp(HitCount, 0, MaxHitCount);
        OnHealthChange?.Invoke(HitCount);

        if (HitCount <= 0)
        {
            IsDead = true;
            OnDie?.Invoke();
        }
    }

    public void Damage(int damage, Transform Instigator)
    {
        RemoveHealth(damage);
    }
}
