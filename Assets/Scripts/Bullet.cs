using System;
using UnityEngine;

namespace MaiNull
{
    public class Bullet : MonoBehaviour
    {
        private const float DestroyTime = 10f;
        private int damage;
        private float bulletSpeed;
        private Vector2 moveDirection;
        private Transform owner = null;
        private Rigidbody2D rb;
        
        public void InitBullet(int damage, float bulletSpeed, Vector2 moveDirection, Transform owner)
        {
            this.damage = damage;
            this.bulletSpeed = bulletSpeed;
            this.moveDirection = moveDirection;
            this.owner = owner;
            
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
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // transform.Translate(moveDirection * (bulletSpeed * Time.deltaTime));
            rb.MovePosition(transform.position + transform.TransformDirection(moveDirection) * (bulletSpeed * Time.deltaTime));
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
            
            if (hitCollider2D.gameObject.CompareTag(owner.gameObject.tag))
            {
                Debug.Log("Same Tag");
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), hitCollider2D);
                return;
            }

            foreach (IDamageable damageable in hitCollider2D.transform.GetComponents<IDamageable>())
            {
                damageable.Damage(damage, owner);
            }
            
            SoundManager.PlaySound(ESoundType.Hit);
            
            Destroy(gameObject);
        }
    }
}