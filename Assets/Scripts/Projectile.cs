using System;
using System.Collections.Generic;
using UnityEngine;
using MaiNull.Utils;
using UnityEngine.Serialization;

namespace MaiNull
{
    public class Projectile : MonoBehaviour
    {
        private Collider2D _collider2D;
        private Vector2 _moveDirection;
        private Transform _owner;
        private ProjectileData _data;
        
        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        public void Init(ProjectileData data, Vector2 moveDirection, Transform owner)
        {
            _data = data;
            _moveDirection = moveDirection;
            _owner = owner;
            
            transform.eulerAngles = new Vector3(0, 0, GameUtils.GetAngleFromVectorFloat(moveDirection));

            if (owner.TryGetComponent(out Collider2D ownerCollider))
            {
                Physics2D.IgnoreCollision(_collider2D, ownerCollider);
            }
            
            // Starts destroy timer
            Destroy(gameObject, data.bulletLifeTime);
        }

        private void Update()
        {
            ApplyMovement();
            CheckCollision();
        }

        private void ApplyMovement()
        {
            transform.Translate(_moveDirection * (_data.bulletSpeed * Time.deltaTime), Space.World);
        }
        
        private void CheckCollision()
        {
            List<Collider2D> results = new();
            
            if (_collider2D.Overlap(ContactFilter2D.noFilter, results) <= 0) return;
            
            foreach (Collider2D c in results)
            {
                if (c.transform.CompareTag(_owner.tag))
                {
                    Physics2D.IgnoreCollision(c, _collider2D);
                    print("Ally collision ignored");
                    continue;
                }
                
                ApplyDamageAndDestroy(c);
            }
        }
        
        private void ApplyDamageAndDestroy(Collider2D hitCollider2D)
        {
            foreach (IDamageable damageable in hitCollider2D.transform.GetComponents<IDamageable>())
            {
                damageable.Damage((int)_data.damage, _owner);
            }
            
            Destroy(gameObject);
        }
    }
}