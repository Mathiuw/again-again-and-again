using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [field: SerializeField] public int Damage { get; set; } = 1;
    public Rigidbody2D Rb { get; private set; }

    public Transform Owner { get; set; }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.CompareTag("Enemy") && Owner.transform.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            return;
        }

        foreach (IDamageable damageable in collision.transform.GetComponents<IDamageable>())
        {
            damageable.Damage(Damage, Owner);
        }

        Destroy(gameObject);
    }
}