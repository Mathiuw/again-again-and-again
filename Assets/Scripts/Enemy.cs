using UnityEngine;

namespace MaiNull
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 3;
        public Health Health;

        private void Awake()
        {
            Health = new Health(maxHealth);
            Health.OnDie += Die;
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        } 

        public void Damage(int damage, Transform instigator)
        {
            Health.RemoveHealth(damage);
        }
    }
}