using Pathfinding;
using UnityEngine;

namespace MaiNull
{
    [RequireComponent(typeof(Animator))]
    public class ZemerlinAnimator : MonoBehaviour
    {
        private AIPath _aiPath;
        private Animator _animator;

        private readonly int _walkFrontHash = Animator.StringToHash("Zehmerlin_Walk_Front");
        private readonly int _walkLeftHash = Animator.StringToHash("Zehmerlin_Walk_Left");
        private readonly int _walkRightHash = Animator.StringToHash("Zehmerlin_Walk_Right");
        private readonly int _walkBackHash = Animator.StringToHash("Zehmerlin_Walk_Back");
        private readonly int _walkIdleHash = Animator.StringToHash("Zehmerlin_Idle");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _aiPath = GetComponentInParent<AIPath>();
        }

        private void Update()
        {
            SetAnimation();
        }

        private void SetAnimation()
        {
            if (!_aiPath || !_animator ) return;
            
            Vector2 moveVector = _aiPath.desiredVelocity.normalized;
            print(moveVector);
            
            if (moveVector == Vector2.zero)
            {
                SetIdleAnimation();
                return;
            }

            const float margin = 0.4f;

            if (moveVector.y <= margin && moveVector.y >= -margin && moveVector.x > 0)
            {
                _animator.Play(_walkRightHash);
            }
            else if (moveVector.y <= margin && moveVector.y >= -margin && moveVector.x < 0)
            {
                _animator.Play(_walkLeftHash);
            }
            else 
            {
                switch (moveVector.y)
                {
                    case > margin:
                        _animator.Play(_walkBackHash);
                        break;
                    case < margin:
                        _animator.Play(_walkFrontHash);
                        break;
                }
            }
        }

        private void SetIdleAnimation()
        {
            AnimatorClipInfo[] animatorClipInfo = _animator.GetCurrentAnimatorClipInfo(0);
            
            _animator.Play(_walkIdleHash);
            
            // TODO: Implement Zemerlin Idle Animation
            // switch (animatorClipInfo[0].clip.name)
            // {
            //     case "player_walk_left":
            //         _animator.Play("player_idle_left");
            //         break;
            //     case "player_walk_right":
            //         _animator.Play("player_idle_right");
            //         break;
            //     case "player_walk_back":
            //         _animator.Play("player_idle_back");
            //         break;
            //     case "player_walk_front":
            //         _animator.Play("player_idle_front");
            //         break;
            // }
        }
    }
}
