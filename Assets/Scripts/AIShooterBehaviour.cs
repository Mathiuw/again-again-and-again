using UnityEngine;

namespace MaiNull
{
    [RequireComponent(typeof(Weapon))]
    public class AIShooterBehaviour : MonoBehaviour
    {
        [Header("Shooter Settings")]
        [SerializeField] private float initialShootDelay = 2f;
        [SerializeField] private float shootFrequency = 3f;
        [SerializeField] private float randomVariation = 1f;
        private Transform _target;
        private Weapon _weapon;

        private void Awake()
        {
            _weapon = GetComponent<Weapon>();
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
            _weapon.Shoot(_target.position);
        }
    }
}