using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletSpeed = 300f;
    [SerializeField] private int damage = 1;
    [SerializeField] private uint bulletAmountPerShot = 1;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private ESoundType _soundType = ESoundType.SHOOT;

    public void Shoot(Vector3 bulletDirection) 
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
            // Set bullet direction and speed
            bullet.Owner = transform;
            bullet.Damage = damage;
            bullet.transform.right = (bulletDirection - transform.position);
            bullet.LaunchBullet(bulletSpeed);

            SoundManager.PlaySound(_soundType);
        }
    }
}
