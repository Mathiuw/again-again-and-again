using System;
using UnityEngine;

namespace MaiNull
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController2D : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 450f;
        private Rigidbody2D _rb;

        public Vector2 MoveVector { get; set; }
        
        public uint MovementSpeedMultiplier { get; set; } = 1;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Vector2 desiredSpeed = MoveVector.normalized * (moveSpeed * MovementSpeedMultiplier);
            
            _rb.linearVelocity = desiredSpeed;
            // print(_rb.linearVelocity);
        }
    }
}
