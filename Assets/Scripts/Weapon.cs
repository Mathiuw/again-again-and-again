using UnityEngine;
using UnityEngine.Serialization;

namespace MaiNull
{
    public class Weapon : MonoBehaviour
    {
        [FormerlySerializedAs("bulletPrefab")] [SerializeField] protected Projectile projectilePrefab;
        [SerializeField] protected float bulletSpeed = 300f;
        [SerializeField] protected int damage = 1;
        [SerializeField] protected bool waitToShoot = true;
        [SerializeField] protected float shootCooldown = 0.75f;
        [SerializeField] protected uint bulletAmountPerShot = 1;
        [SerializeField] protected Transform[] spawnPositions;
        protected float CurrentCooldown = 0f;
        
        private void Update()
        {
            if (!(CurrentCooldown > 0f)) return;
            
            CurrentCooldown -= Time.deltaTime;
            CurrentCooldown = Mathf.Max(CurrentCooldown, 0);
        }
        
        public void Shoot(Vector2 bulletDirection) 
        {
            Vector3 spawnPosition = transform.position;

            for (int i = 0; i < bulletAmountPerShot; i++)
            {
                if (i <= spawnPositions.Length -1)
                {
                    spawnPosition = spawnPositions[i].position;
                }

                // Spawn bullet
                Projectile projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity, null);
                // Init bullet
                projectile.Init(damage, bulletSpeed, bulletDirection, transform);
            }
        }
    }
}
