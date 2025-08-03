using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    public Image barImage;         // Assign the UI Image in Inspector
    public float totalTime = 180f; // 3 minutes in seconds
    private float timeRemaining;

    void Start()
    {
        timeRemaining = totalTime;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            float fillAmount = Mathf.Clamp01(timeRemaining / totalTime);
            barImage.fillAmount = fillAmount;
        }
        else
        {
            barImage.fillAmount = 0f;
            // Timer finished, trigger any event here if needed
        }
    }

    // Optional: Call this method to reset the timer
    public void ResetTimer()
    {
        timeRemaining = totalTime;
    }
}