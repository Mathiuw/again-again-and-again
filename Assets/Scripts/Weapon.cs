using UnityEngine;

namespace MaiNull
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected Bullet bulletPrefab;
        [SerializeField] protected float bulletSpeed = 300f;
        [SerializeField] protected int damage = 1;
        [SerializeField] protected bool waitToShoot = true;
        [SerializeField] protected float shootCooldown = 0.75f;
        [SerializeField] protected uint bulletAmountPerShot = 1;
        [SerializeField] protected Transform[] spawnPositions;
        [SerializeField] protected ESoundType soundType = ESoundType.Shoot;
        protected float currentCooldown = 0f;
        
        private void Update()
        {
            if (currentCooldown > 0f)
            {
                currentCooldown -= Time.deltaTime;
                currentCooldown = Mathf.Max(currentCooldown, 0);
            }
        }
        
        public void Shoot(Vector2 bulletDirection) 
        {
            Vector3 spawnPosition = transform.position;

            for (int i = 0; i < bulletAmountPerShot; i++)
            {
                if (spawnPositions.Length < i - 1)
                {
                    spawnPosition = spawnPositions[i].position;
                }

                // Spawn bullet
                Bullet bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity, null);
                // Init bullet
                bullet.Init(damage, bulletSpeed, bulletDirection, transform);

                SoundManager.PlaySound(soundType);
            }
        }
    }
}
