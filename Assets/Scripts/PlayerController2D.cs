using UnityEngine;
using UnityEngine.InputSystem;

namespace MaiNull
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController2D : MonoBehaviour
    {
        [SerializeField]private InputActionReference moveInputAction;

        [field: SerializeField] public float MoveSpeed { get; private set; } = 250f;

        public Vector2 MoveVector { get; private set; } = Vector2.zero;

        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            moveInputAction.action.performed += OnMovePerformed;
            moveInputAction.action.canceled += OnMoveCanceled;
            moveInputAction.action.Enable();
        }

        private void OnDisable()
        {
            moveInputAction.action.performed -= OnMovePerformed;
            moveInputAction.action.canceled -= OnMoveCanceled;
            moveInputAction.action.Disable();
        }

        private void FixedUpdate()
        {
            Vector2 desiredSpeed = MoveVector.normalized * (MoveSpeed * Time.deltaTime);

            rb.linearVelocity = desiredSpeed;
            //Debug.Log(rb.linearVelocity);
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            MoveVector = context.ReadValue<Vector2>();
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            MoveVector = Vector2.zero;
        }
    }
}
