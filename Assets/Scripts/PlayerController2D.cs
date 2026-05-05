using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace MaiNull
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController2D : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 450f;
        
        [Header("Dash")] 
        [SerializeField] private float speedMultiplier = 6f;
        [SerializeField] private float duration;
        [SerializeField] private float cooldown; 
        private Vector2 _dashDirection;
        private Dash _dash;
        
        private Rigidbody2D _rb;

        public Vector2 MoveDirection { get; set; }

        private float MovementSpeedMultiplier => 1 * _dash.CurrentSpeedMultiplier;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _dash = new Dash(speedMultiplier);
        }

        private void Update()
        {
            Vector2 desiredDirection;
            float speed = moveSpeed * MovementSpeedMultiplier;

            if (!_dash.IsDashing)
            {
                desiredDirection = MoveDirection.normalized * speed;
            }
            else desiredDirection = _dashDirection.normalized * speed;
            
            _rb.linearVelocity = desiredDirection;
            // print(_rb.linearVelocity);
        }

        public void TryDash()
        {
            if (!_dash.CanDash()) return;

            _dashDirection = MoveDirection;
            
            StartCoroutine(_dash.DashCoroutine(duration, cooldown));    
        }
    }
}
