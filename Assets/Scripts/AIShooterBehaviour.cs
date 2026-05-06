using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MaiNull
{
    [RequireComponent(typeof(Gun))]
    public class AIShooterBehaviour : MonoBehaviour
    {
        [Header("Shooter Settings")]
        [SerializeField] private float initialShootDelay = 2f;
        [SerializeField] private float shootFrequency = 3f;
        [SerializeField] private float randomVariation = 1f;
        [SerializeField] private bool shootGunComponent = true;
        private Transform _target;
        private Gun _gun;

        public float ShootDuration { get; set; } = 0;
        
        public Action OnAIShoot;
        
        private void Awake()
        {
            _gun = GetComponent<Gun>();
        }

        protected void OnEnable()
        {
            float randomShootFrequency = Random.Range(-randomVariation, randomVariation);
			
            InvokeRepeating(nameof(TriggerAIShoot), initialShootDelay + randomShootFrequency, shootFrequency + randomShootFrequency + ShootDuration);
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
            CancelInvoke(nameof(TriggerAIShoot));
        }

        private void TriggerAIShoot()
        {
            if (shootGunComponent)
            {
                ShootTarget();
            }
            
            OnAIShoot?.Invoke();
        }
        
        public void ShootTarget() 
        {
            _gun.Shoot((_target.position - transform.position).normalized);
        }
    }
}