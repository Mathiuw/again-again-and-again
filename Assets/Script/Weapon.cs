using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet _bulletPrefab;
    [SerializeField] protected float _bulletSpeed = 300f;
    [SerializeField] protected int _damage = 1;

    public void ShootBullet(Vector3 bulletDirection, Transform spawnPosition = null) 
    {
        Vector3 spawn = transform.position;

        if (spawnPosition != null)
        {
            spawn = spawnPosition.position;
        }

        // Spawn bullet
        Bullet bullet = Instantiate(_bulletPrefab, spawn, Quaternion.identity, null);
        // Set bullet direction and speed
        bullet.Owner = transform;
        bullet.Damage = _damage;
        bullet.transform.right = (bulletDirection - transform.position);
        bullet.Rb.AddForce(bullet.transform.right * _bulletSpeed);

        Collider2D shooterCollider = GetComponent<Collider2D>();
        if (shooterCollider)
        {
            // Bullet ignore shooter
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), shooterCollider);
        }

        SoundManager.PlaySound(ESoundType.SHOOT);
    }
}
