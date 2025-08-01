using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class WeaponPlayer : Weapon
{
    [SerializeField] protected Transform _orientationTransform;
    [SerializeField] protected float _shootCooldown = 0.75f;
    private float _currentCooldown = 0f;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();

        _playerMovement.Input.Player.Attack.started += OnAttackStarted;
    }

    private void Update()
    {
        if (_currentCooldown > 0f)
        {
            _currentCooldown -= Time.deltaTime;
            _currentCooldown = Mathf.Clamp(_currentCooldown, 0, _shootCooldown);
        }
    }

    private void OnDisable()
    {
        _playerMovement.Input.Player.Attack.started -= OnAttackStarted;
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        if (_currentCooldown <= 0f)
        {
            ShootBullet();
            _currentCooldown = _shootCooldown;
        }
    }

    public void ShootBullet()
    {
        // Spawn bullet
        Bullet bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity, null);
        // Set bullet direction and speed
        bullet.Owner = transform;
        bullet.Damage = _damage;
        bullet.transform.right = _orientationTransform.right;
        bullet.Rb.AddForce(bullet.transform.right * _bulletSpeed);

        Collider2D shooterCollider = GetComponent<Collider2D>();
        if (shooterCollider)
        {
            // Bullet ignore shooter
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), shooterCollider);
        }

        if (_shootSFX)
        {
            _shootSFX.Play();
        }
    }
}