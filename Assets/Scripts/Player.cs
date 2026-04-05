using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public delegate void PlayerDead();
    public static event PlayerDead OnPlayerDie;
    private Timer timer;

    private void Start()
    {
        timer = FindFirstObjectByType<Timer>();
        if (timer)
        {
            timer.OnTimerEnd += Die;
        }
    }

    private void OnDisable()
    {
        if (timer)
        {
            timer.OnTimerEnd -= Die;
        }
    }

    private void Die()
    {
        PlayerController2D playerMovement = GetComponent<PlayerController2D>();
        if (playerMovement)
        {
            playerMovement.enabled = false;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        OnPlayerDie?.Invoke();
    }

    public void Damage(int damage, Transform Instigator)
    {
        if (timer)
        {
            timer.RemoveTime(damage);
        }
    }
}