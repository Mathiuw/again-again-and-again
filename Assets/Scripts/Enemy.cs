using UnityEngine;


public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 3;
    public Health health;

    private void Awake()
    {
        health = new(maxHealth);
        health.OnDie += Die;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void Damage(int damage, Transform Instigator)
    {
        health.RemoveHealth(damage);
    }
}