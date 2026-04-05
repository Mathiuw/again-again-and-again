using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    public Health health;

    private void Awake()
    {
        health = new(maxHealth);
        health.OnDie += Die;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}