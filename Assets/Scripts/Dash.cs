using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MaiNull
{
    public class Dash : MonoBehaviour
    {
        [SerializeField] private InputActionReference dashInputAction;
        [SerializeField] private int dashSpeedMultiplier;
        [SerializeField] private float dashDuration;
        [SerializeField] private float cooldown = 0;
        private bool _canDash = true;
        private PlayerController2D _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController2D>();
        }

        private void OnEnable()
        {
            dashInputAction.action.started += OnDashStarted;
            dashInputAction.action.Enable();
        }

        private void OnDisable()
        {
            dashInputAction.action.started -= OnDashStarted;
            dashInputAction.action.Disable();
        }

        private void OnDashStarted(InputAction.CallbackContext obj)
        {
            if (!_playerController || !_canDash) return;
            
            _playerController.MovementSpeedMultiplier += (uint)dashSpeedMultiplier;
            Invoke(nameof(ResetSpeedMultiplier), dashDuration);

            if (cooldown <= 0) return;
            
            _canDash = false;
            Invoke(nameof(ActivateDash), cooldown);
        }

        private void ActivateDash()
        {
            _canDash = true;
        }
        
        private void ResetSpeedMultiplier()
        {
            if (_playerController)
            {
                _playerController.MovementSpeedMultiplier -= (uint)dashSpeedMultiplier;
            }
        }
    }
}