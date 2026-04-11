using UnityEngine;

namespace MaiNull
{
    public class PlayerAnimator : MonoBehaviour
    {
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
            SetAnimation();
            
            // AnimatorClipInfo[] animatorClipInfo = _animator.GetCurrentAnimatorClipInfo(0);
            // Debug.Log(animatorClipInfo[0].clip.name);
        }

        private void SetAnimation()
        {
            Vector2 moveVector = _playerController.MoveVector;

            if (moveVector == Vector2.zero)
            {
                SetIdleAnimation();
                return;
            }

            if (Mathf.Approximately(moveVector.x, 1))
            {
                _animator.Play("player_walk_right");
            }
            else if (Mathf.Approximately(moveVector.x, -1))
            {
                _animator.Play("player_walk_left");
            }
            else if (Mathf.Approximately(moveVector.y, 1))
            {
                _animator.Play("player_walk_back");
            }
            else if (Mathf.Approximately(moveVector.y, -1))
            {
                _animator.Play("player_walk_front");
            }
        }

        private void SetIdleAnimation()
        {
            AnimatorClipInfo[] animatorClipInfo = _animator.GetCurrentAnimatorClipInfo(0);
            
            switch (animatorClipInfo[0].clip.name)
            {
                case "player_walk_left":
                    _animator.Play("player_idle_left");
                    break;
                case "player_walk_right":
                    _animator.Play("player_idle_right");
                    break;
                case "player_walk_back":
                    _animator.Play("player_idle_back");
                    break;
                case "player_walk_front":
                    _animator.Play("player_idle_front");
                    break;
            }
        }
        
        private void OnPlayerDie()
        {
            _animator.Play("player_die");
        }
    }
}
