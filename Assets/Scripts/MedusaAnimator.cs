using UnityEngine;

namespace MaiNull
{
    public class MedusaAnimator: MonoBehaviour
    {
        private Animator animator;
        private AIShooterBehaviour aiShooterBehaviour;

        private int ShootAnimationHash = Animator.StringToHash("Medusa_Shoot");

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            aiShooterBehaviour = GetComponent<AIShooterBehaviour>();
        }

        public void StartShootAnimation()
        {
            animator.Play(ShootAnimationHash);
        }

        public void MedusaShootEvent()
        {
            aiShooterBehaviour.ShootTarget();
        }
    }
}
