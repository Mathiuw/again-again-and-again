using Pathfinding;
using UnityEngine;

namespace MaiNull
{
    [RequireComponent(typeof(Animator))]
    public class ZemerlinAnimator : MonoBehaviour
    {
        private AIPath _aiPath;
        private Animator _animator;

        private readonly int _walkFrontHash = Animator.StringToHash("Zehmerlin_Walk_Back");
        private readonly int _walkLeftHash = Animator.StringToHash("Zehmerlin_Walk_Back");
        private readonly int _walkRightHash = Animator.StringToHash("Zehmerlin_Walk_Back");
        private readonly int _walkBackHash = Animator.StringToHash("Zehmerlin_Walk_Back");
        private readonly int _walkIdleHash = Animator.StringToHash("Zehmerlin_Idle");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _aiPath = GetComponentInParent<AIPath>();
        }

        private void Update()
        {
            ApplyAnimation();
        }

        private void ApplyAnimation()
        {
            if (!_aiPath || !_animator ) return;
            
            switch (_aiPath.velocity.y)
            {
                case > 0:
                    _animator.Play(_walkBackHash);
                    break;
                case < 0:
                    _animator.Play(_walkFrontHash);
                    break;
                default:
                {
                    switch (_aiPath.velocity.x)
                    {
                        case > 0:
                            _animator.Play(_walkRightHash);
                            break;
                        case < 0:
                            _animator.Play(_walkLeftHash);
                            break;
                        default:
                            _animator.Play(_walkIdleHash);
                            break;
                    }

                    break;
                }
            }
        }
    }
}
