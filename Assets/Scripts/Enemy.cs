using UnityEngine;

namespace MaiNull
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 3;
        public Health health;

        private void Awake()
        {
            health = new Health(maxHealth);
            health.OnDie += Die;
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        } 

        public void Damage(int damage, Transform instigator)
        {
            health.RemoveHealth(damage);
        }
    }
}