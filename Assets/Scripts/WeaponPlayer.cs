using UnityEngine;
using UnityEngine.InputSystem;

namespace MaiNull
{
    [RequireComponent(typeof(PlayerController2D))]
    public class WeaponPlayer : Weapon
    {
        [SerializeField] protected Transform orientationTransform;
        [SerializeField] private InputActionReference shootInputAction;

        private void OnEnable()
        {
            shootInputAction.action.started += OnAttackStarted;
            shootInputAction.action.Enable();
        }

        private void OnDisable()
        {
            shootInputAction.action.started -= OnAttackStarted;
            shootInputAction.action.Disable();
        }

        private void OnAttackStarted(InputAction.CallbackContext context)
        {
            Vector2 moveVector = context.ReadValue<Vector2>();
            Vector3 desiredRotation = Vector3.zero;

            if (Mathf.Approximately(moveVector.x, 1))
            {
                desiredRotation.z = 0;
                orientationTransform.rotation = Quaternion.Euler(desiredRotation);
            }
            else if (Mathf.Approximately(moveVector.x, -1))
            {
                desiredRotation.z = 180;
                orientationTransform.rotation = Quaternion.Euler(desiredRotation);
            }
            else if (Mathf.Approximately(moveVector.y, 1))
            {
                desiredRotation.z = 90;
                orientationTransform.rotation = Quaternion.Euler(desiredRotation);
            }
            else if (Mathf.Approximately(moveVector.y, -1))
            {
                desiredRotation.z = -90;
                orientationTransform.rotation = Quaternion.Euler(desiredRotation);
            }

            if (currentCooldown <= 0f)
            {
                Shoot(orientationTransform.right);
            }
        }
    }
}