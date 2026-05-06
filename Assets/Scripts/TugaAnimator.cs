using UnityEngine;

namespace MaiNull
{
    [RequireComponent(typeof(Animator))]
    public class TugaAnimator : MonoBehaviour
    {
        private Animator _animator;
        private AIShooterBehaviour _aiShooterBehaviour;

        private readonly int _shootAnimationHash = Animator.StringToHash("Tuga_Shoot");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _aiShooterBehaviour = GetComponentInParent<AIShooterBehaviour>();
        }

        private void Start()
        {
            if (!_aiShooterBehaviour) return;
            
            _aiShooterBehaviour.OnAIShoot += OnAIShoot;
            _aiShooterBehaviour.ShootDuration = _animator.runtimeAnimatorController.animationClips[1].length;
            print(_animator.runtimeAnimatorController.animationClips[1].name);
        }

        private void OnAIShoot()
        {
            _animator.Play(_shootAnimationHash);
        }

        public void ShootEvent()
        {
            _aiShooterBehaviour.ShootTarget();
        }

        private void OnDeathAnimationOver()
        {
            Destroy(GetComponentInParent<Enemy>().gameObject);
        }
        
        private void PlayAnimation(string clipName)
        {
            _animator.Play(clipName);
        }
    }
}
