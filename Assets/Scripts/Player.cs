using UnityEngine;

namespace MaiNull
{
    public class Player : MonoBehaviour, IDamageable
    {
        public delegate void PlayerDead();
        public static event PlayerDead OnPlayerDie;
        private LoopTimer loopTimer;

        private void Start()
        {
            LoopTimer.OnTimerEnd += Die;
        }

        private void OnDisable()
        {
            LoopTimer.OnTimerEnd -= Die;
        }

        private void Die()
        {
            PlayerController2D playerMovement = GetComponent<PlayerController2D>();
            if (playerMovement)
            {
                playerMovement.enabled = false;
            }

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
            }

            OnPlayerDie?.Invoke();
        }

        public void Damage(int damage, Transform Instigator)
        {
            if (loopTimer)
            {
                LoopTimer.RemoveTime(damage);
            }
        }
    }
}