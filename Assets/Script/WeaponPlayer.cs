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

        if (_playerMovement.Input.Player.Attack.IsPressed() && _currentCooldown <= 0)
        {
            ShootBullet();
            _currentCooldown = _shootCooldown;
        }
    }

    private void OnDisable()
    {
        _playerMovement.Input.Player.Attack.started -= OnAttackStarted;
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        Vector3 desiredRotaion = Vector3.zero;

        if (moveVector.x == 1)
        {
            desiredRotaion.z = 0;
            _orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
        }
        else if (moveVector.x == -1)
        {
            desiredRotaion.z = 180;
            _orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
        }
        else if (moveVector.y == 1)
        {
            desiredRotaion.z = 90;
            _orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
        }
        else if (moveVector.y == -1)
        {
            desiredRotaion.z = -90;
            _orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
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

        SoundManager.PlaySound(ESoundType.BOWAUDIO);
    }
}