using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MaiNull
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController2D : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveInputAction;
        [SerializeField] private float moveSpeed = 450f;
        private Vector2 _moveVector = Vector2.zero;
        private Rigidbody2D _rb;

        public Vector2 MoveVector => _moveVector;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
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
            Vector2 desiredSpeed = _moveVector.normalized * (moveSpeed * Time.deltaTime);

            _rb.linearVelocity = desiredSpeed;
            //Debug.Log(rb.linearVelocity);
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _moveVector = context.ReadValue<Vector2>();
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            _moveVector = Vector2.zero;
        }
    }
}
