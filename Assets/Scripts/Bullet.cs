using System;
using UnityEngine;

namespace MaiNull
{
    public class Bullet : MonoBehaviour
    {
        private const float DestroyTime = 10f;
        private int _damage;
        private float _bulletSpeed;
        private Vector2 _moveDirection;
        private Transform _owner = null;
        private Rigidbody2D _rb;
        
        public void Init(int damage, float bulletSpeed, Vector2 moveDirection, Transform owner)
        {
            this._damage = damage;
            this._bulletSpeed = bulletSpeed;
            this._moveDirection = moveDirection;
            this._owner = owner;
            
            transform.eulerAngles = new Vector3(0, 0,  GetAngleFromVectorFloat(moveDirection));
            
            // Bullet ignore shooter
            Collider2D ownerCollider = owner.GetComponent<Collider2D>();
            if (ownerCollider)
            {
                // Physics2D.IgnoreCollision(bulletCollider2D, ownerCollider);
            }
            
            // Starts destroy timer
            Destroy(gameObject, DestroyTime);
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // transform.Translate(moveDirection * (bulletSpeed * Time.deltaTime));
            _rb.MovePosition(transform.position + (Vector3)_moveDirection * (_bulletSpeed * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            ApplyDamageAndDestroy(other);
        }

        private float GetAngleFromVectorFloat(Vector3 dir)
        {
            dir = dir.normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (angle < 0)
            {
                angle += 360f;
            }
            return angle;   
        }
        
        private void ApplyDamageAndDestroy(Collider2D hitCollider2D)
        {
            if (!hitCollider2D) return;
            
            if (hitCollider2D.gameObject.CompareTag(_owner.gameObject.tag))
            {
                Debug.Log("Same Tag");
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), hitCollider2D);
                return;
            }

            foreach (IDamageable damageable in hitCollider2D.transform.GetComponents<IDamageable>())
            {
                damageable.Damage(_damage, _owner);
            }
            
            SoundManager.PlaySound(ESoundType.Hit);
            
            Destroy(gameObject);
        }
    }
}