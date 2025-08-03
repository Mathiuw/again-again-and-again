using UnityEngine;

public class TimeCollectable : MonoBehaviour
{
    public float additionalTime = 15f; // Adjustable in Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Timer timer = FindAnyObjectByType<Timer>(); // Looks for your Timer script
            if (timer != null)
                timer.AddTime(additionalTime);

            Destroy(gameObject);
        }
    }
}