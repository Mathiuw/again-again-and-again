using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MaiNull
{
    public class Player : MonoBehaviour, IDamageable
    {
        [Header("Input")]
        [SerializeField] private InputActionReference shootInputAction;
        [SerializeField] private InputActionReference moveInputAction;
        [SerializeField] private InputActionReference dashInputAction;
        private PlayerController2D _playerController;
        private WeaponManager _weaponManager;
        
        public static Action OnPlayerDie;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController2D>();
            _weaponManager = GetComponent<WeaponManager>();
        }

        private void OnEnable()
        {
            EnableInput();
        }

        private void Start()
        {
            LoopTimer.OnLoopEnd += Die;
        }

        private void Update()
        {
            // Shoot logic
            if (shootInputAction.action.IsPressed())
            {
                PerformAttack();
            }
        }

        private void OnDisable()
        {
            LoopTimer.OnLoopEnd -= Die;

            DisableInput();
        }

        private void PerformAttack()
        {
            if (!_weaponManager) return;
            
            Vector2 shootDirection = shootInputAction.action.ReadValue<Vector2>();
            
            _weaponManager.Shoot(shootDirection);
            
            print("Weapon Shoot Performed");
        }
        
        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _playerController.MoveVector = context.ReadValue<Vector2>();
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            _playerController.MoveVector = Vector2.zero;
        }
        
        private void OnDashStarted(InputAction.CallbackContext obj)
        {
            if (TryGetComponent(out Dash dash))
            {
                dash.StartDash();
            }
        }
        
        private void Die()
        {
            DisableInput();
            
            if (TryGetComponent(out Rigidbody2D rb))
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
            }

            OnPlayerDie?.Invoke();
        }
        
        public void Damage(int damage, Transform instigator)
        {
            LoopTimer.RemoveTime(damage);
        }

        public void EnableInput()
        {
            shootInputAction.action.Enable();
            
            moveInputAction.action.performed += OnMovePerformed;
            moveInputAction.action.canceled += OnMoveCanceled;
            moveInputAction.action.Enable();
            
            dashInputAction.action.started += OnDashStarted;
            dashInputAction.action.Enable();
        }

        public void DisableInput()
        {
            shootInputAction.action.Disable();
            
            moveInputAction.action.Disable();
            moveInputAction.action.performed -= OnMovePerformed;
            moveInputAction.action.canceled -= OnMoveCanceled;
            
            dashInputAction.action.Disable();
            dashInputAction.action.started -= OnDashStarted;
        }
    }
}