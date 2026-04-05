using UnityEngine;

public class Tuga : MonoBehaviour
{
    [Header("Tuga Settings")]
    private int maxHealth = 5;
    private Health health;


    private void Awake()
    {
        health = new(maxHealth);
    }

    private void OnEnable()
    {
        _tugaAnimationManager = GetComponentInChildren<TugaAnimator>();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
