using UnityEngine;
using UnityEngine.Serialization;

namespace MaiNull
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private ProjectileData projectileData;
        [SerializeField] private bool hasCooldown = true;
        [SerializeField] private float shootCooldown = 0.75f;
        [SerializeField] private uint bulletAmountPerShot = 1;
        [SerializeField] private Transform[] spawnPositions;
        private bool _canShoot = true;

        public float CurrentCooldown { get; private set; } = 0f;
        
        private void Update()
        {
            CurrentCooldown -= Time.deltaTime;
            CurrentCooldown = Mathf.Max(CurrentCooldown, 0f);

            if (!_canShoot && CurrentCooldown <= 0f)
            {
                _canShoot = true;
            }
        }

        public void Shoot(Vector2 bulletDirection) 
        {
            if (!_canShoot) return;
            
            Vector3 spawnPosition = transform.position;

            for (int i = 0; i < bulletAmountPerShot; i++)
            {
                if (i <= spawnPositions.Length -1)
                {
                    spawnPosition = spawnPositions[i].position;
                }
                
                // Spawn and Init bullet
                if (Instantiate(projectileData.prefab, spawnPosition, Quaternion.identity, null).TryGetComponent(out Projectile projectile))
                {
                    projectile.Init(projectileData, bulletDirection, transform);
                }
            }

            if (!hasCooldown) return;

            _canShoot = false;
            CurrentCooldown = shootCooldown;
        }
    }
}
