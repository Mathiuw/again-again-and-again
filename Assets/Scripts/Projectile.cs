using System.Collections.Generic;
using UnityEngine;
using MaiNull.Utils;

namespace MaiNull
{
    public class Projectile : MonoBehaviour
    {
        [Header("Collision Settings")] 
        [SerializeField] private Vector2 size;
        [SerializeField] private Vector2 pivotOffset;
        [SerializeField] private ContactFilter2D contactFilter;
        
        private const float DestroyTime = 10f;
        private int _damage;
        private float _bulletSpeed;
        private Vector2 _moveDirection;
        private Transform _owner;

        public void Init(int damage, float bulletSpeed, Vector2 moveDirection, Transform owner)
        {
            _damage = damage;
            _bulletSpeed = bulletSpeed;
            _moveDirection = moveDirection;
            _owner = owner;
            
            transform.eulerAngles = new Vector3(0, 0, GameUtils.GetAngleFromVectorFloat(moveDirection));
            
            // Starts destroy timer
            Destroy(gameObject, DestroyTime);
        }

        private void Update()
        {
            ApplyMovement();
            CheckCollision();
        }

        private void ApplyMovement()
        {
            transform.Translate(_moveDirection * (_bulletSpeed * Time.deltaTime), Space.World);
        }
        
        private void CheckCollision()
        {
            List<Collider2D> results = new List<Collider2D>();
            
            if (Physics2D.OverlapBox(transform.position + (Vector3)pivotOffset, size, transform.eulerAngles.z, contactFilter, results) <= 0) return;
            
            foreach (Collider2D c in results)
            {
                if (_owner && c.transform == _owner)
                {
                    print("Owner Collision Ignore");
                    continue;
                }
                if (c.transform.CompareTag(_owner.tag))
                {
                    print("Ally Collision");
                    continue;
                }
                
                ApplyDamageAndDestroy(c);
            }
        }
        
        private void ApplyDamageAndDestroy(Collider2D hitCollider2D)
        {
            foreach (IDamageable damageable in hitCollider2D.transform.GetComponents<IDamageable>())
            {
                damageable.Damage(_damage, _owner);
            }
            
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(pivotOffset, size);
        }
    }
}