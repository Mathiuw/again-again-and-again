using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationManager_Player : AnimationManager_Base
{
    [SerializeField] private Transform _orientationTransform;
    PlayerMovement _playerMovement;

    protected new void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Health health = GetComponent<Health>();
        if (health)
        {
            health.OnDie += OnDie;
        }

        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Input.Player.Move.performed += OnMovePerformed;
        _playerMovement.Input.Player.Move.canceled += OnMoveCanceled;
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
            ChangeAnimation("player_walk_left");
        }
        else if (moveVector.x == -1)
        {
            //desiredRotaion.z = 180;
           // _orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
            ChangeAnimation("player_walk_right");
        }
        else if (moveVector.y == 1)
        {
           // desiredRotaion.z = 90;
           // _orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
            ChangeAnimation("player_walk_back");
        }
        else if (moveVector.y == -1)
        {
           // desiredRotaion.z = -90;
           // _orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
            ChangeAnimation("player_walk_front");
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        ChangeAnimation("player_idle");
    }

    private void OnDie()
    {
        ChangeAnimation("player_die");
    }
}
