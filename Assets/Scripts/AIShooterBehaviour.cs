using UnityEngine;

namespace MaiNull
{
    [RequireComponent(typeof(Gun))]
    public class AIShooterBehaviour : MonoBehaviour
    {
        [Header("Shooter Settings")]
        [SerializeField] private float initialShootDelay = 2f;
        [SerializeField] private float shootFrequency = 3f;
        [SerializeField] private float randomVariation = 1f;
        private Transform _target;
        private Gun _gun;

        private void Awake()
        {
            _gun = GetComponent<Gun>();
        }

        protected void OnEnable()
        {
            float randomShootFrequency = Random.Range(-randomVariation, randomVariation);
			
            InvokeRepeating(nameof(ShootTarget), initialShootDelay + randomShootFrequency, shootFrequency + randomShootFrequency);
        }

        private void Start()
        {
            Transform playerTransform = FindFirstObjectByType<Player>()?.transform;
            if (playerTransform)
            {
                _target = playerTransform;
            }
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(ShootTarget));
        }

        public void ShootTarget() 
        {
            _gun.Shoot((_target.position - transform.position).normalized);
        }
    }
}