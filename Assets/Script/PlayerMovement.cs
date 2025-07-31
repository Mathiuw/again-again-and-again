using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public InputSystem_Actions Input { get; private set; }

    [field: SerializeField] public float Speed { get; private set; } = 250f;
    public Vector2 MoveVector { get; private set; } = Vector2.zero;
    Rigidbody2D rb;

    private void Awake()
    {
        Input = new InputSystem_Actions();

        rb = GetComponent<Rigidbody2D>();

        Input.Player.Move.performed += OnMovePerformed;
        Input.Player.Move.canceled += OnMoveCanceled;
        Input.Enable();
    }

    private void OnDisable()
    {
        Input.Player.Move.performed -= OnMovePerformed;
        Input.Player.Move.canceled -= OnMoveCanceled;
        Input.Disable();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 desiredSpeed = MoveVector.normalized * Speed * Time.deltaTime;

        rb.linearVelocity = desiredSpeed;
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
