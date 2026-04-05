using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController2D))]
public class WeaponPlayer : Weapon
{
    [SerializeField] protected Transform orientationTransform;
    [SerializeField] protected float shootCooldown = 0.75f;
    [SerializeField] private InputActionReference shootInputAction;
    private float currentCooldown = 0f;

    private void OnEnable()
    {
        shootInputAction.action.started += OnAttackStarted;
        shootInputAction.action.Enable();
    }

    private void Update()
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
            currentCooldown = Mathf.Max(currentCooldown, 0);
        }
    }

    private void OnDisable()
    {
        shootInputAction.action.started -= OnAttackStarted;
        shootInputAction.action.Disable();
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        Vector3 desiredRotaion = Vector3.zero;

        if (moveVector.x == 1)
        {
            desiredRotaion.z = 0;
            orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
        }
        else if (moveVector.x == -1)
        {
            desiredRotaion.z = 180;
            orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
        }
        else if (moveVector.y == 1)
        {
            desiredRotaion.z = 90;
            orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
        }
        else if (moveVector.y == -1)
        {
            desiredRotaion.z = -90;
            orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
        }

        if (currentCooldown <= 0f)
        {
            ShootBullet();
        }
    }

    public void ShootBullet()
    {
        // Spawn bullet
        Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, null);
        // Set bullet direction and speed
        bullet.Owner = transform;
        bullet.Damage = damage;
        bullet.transform.right = orientationTransform.right;
        bullet.Rb.AddForce(bullet.transform.right * bulletSpeed);

        Collider2D shooterCollider = GetComponent<Collider2D>();
        if (shooterCollider)
        {
            // Bullet ignore shooter
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), shooterCollider);
        }

        SoundManager.PlaySound(ESoundType.BOWAUDIO);
    }
}