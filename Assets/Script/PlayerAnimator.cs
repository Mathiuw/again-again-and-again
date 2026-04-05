using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Transform _orientationTransform;
    PlayerController2D playerController;
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Player.OnPlayerDead += OnPlayerDie;

        playerController = GetComponent<PlayerController2D>();
    }

    private void Update()
    {
        ApplyAnimations();
    }

    private void ApplyAnimations()
    {
        Vector2 moveVector = playerController.MoveVector;
        Vector3 desiredRotaion = Vector3.zero;

        if (moveVector == Vector2.zero)
        {
            animator.Play("player_idle");
        }

        if (moveVector.x == 1)
        {
            animator.Play("player_walk_left");
        }
        else if (moveVector.x == -1)
        {
            //desiredRotaion.z = 180;
            // _orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
            animator.Play("player_walk_right");
        }
        else if (moveVector.y == 1)
        {
            // desiredRotaion.z = 90;
            // _orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
            animator.Play("player_walk_back");
        }
        else if (moveVector.y == -1)
        {
            // desiredRotaion.z = -90;
            // _orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
            animator.Play("player_walk_front");
        }
    }

    private void OnPlayerDie()
    {
        animator.Play("player_die");
    }
}
