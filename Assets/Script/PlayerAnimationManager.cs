using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private Transform _orientationTransform;
    PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Input.Player.Move.performed += OnMovePerformed;
    }

    private void OnDisable()
    {
        _playerMovement.Input.Player.Move.performed -= OnMovePerformed;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
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
}
