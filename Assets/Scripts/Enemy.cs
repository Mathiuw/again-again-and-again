using System.Collections.Generic;
using UnityEngine;

namespace MaiNull
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public static readonly List<Enemy> EnemiesList = new();
        
        [SerializeField] protected int maxHealth = 3;
        public Health Health;

        private void Awake()
        {
            Health = new Health(maxHealth);
            Health.OnDie += Die;
        }

        private void OnEnable()
        {
            EnemiesList.Add(this);
        }

        private void OnDisable()
        {
            EnemiesList.Remove(this);
        }

        protected virtual void Die()
        {
            EnemiesList.Remove(this);
            Destroy(gameObject);
        } 

        public void Damage(int damage, Transform instigator)
        {
            Health.RemoveHealth(damage);
        }
    }
}