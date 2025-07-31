using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class Weapon : MonoBehaviour
{
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] float _bulletSpeed = 300f;
    [SerializeField] Transform _orientationTransform;
    PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();

        _playerMovement.Input.Player.Attack.started += OnAttackStarted;
    }

    private void OnDisable()
    {
        _playerMovement.Input.Player.Attack.started -= OnAttackStarted;
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        // Spawn bullet
        Bullet bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity, null);
        // Set bullet direction and speed
        bullet.transform.right = _orientationTransform.right;
        bullet.Rb.AddForce(_orientationTransform.right * _bulletSpeed);

        // Bullet ignore player
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}
