using UnityEngine;
using UnityEngine.Serialization;

namespace MaiNull
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Transform orientationTransform;
        private PlayerController2D _playerController;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            Player.OnPlayerDie += OnPlayerDie;

            _playerController = GetComponent<PlayerController2D>();
        }

        private void Update()
        {
            ApplyAnimations();
        }

        private void ApplyAnimations()
        {
            Vector2 moveVector = _playerController.MoveVector;
            Vector3 desiredRotation = Vector3.zero;

            if (moveVector == Vector2.zero)
            {
                _animator.Play("player_idle");
            }

            if (Mathf.Approximately(moveVector.x, 1))
            {
                _animator.Play("player_walk_left");
            }
            else if (Mathf.Approximately(moveVector.x, -1))
            {
                //desiredRotaion.z = 180;
                // _orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
                _animator.Play("player_walk_right");
            }
            else if (Mathf.Approximately(moveVector.y, 1))
            {
                // desiredRotaion.z = 90;
                // _orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
                _animator.Play("player_walk_back");
            }
            else if (Mathf.Approximately(moveVector.y, -1))
            {
                // desiredRotaion.z = -90;
                // _orientationTransform.rotation = Quaternion.Euler(desiredRotaion);
                _animator.Play("player_walk_front");
            }
        }

        private void OnPlayerDie()
        {
            _animator.Play("player_die");
        }
    }
}
