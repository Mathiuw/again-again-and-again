using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public float totalTime = 180f;
    public Text timerText;

    private float timeLeft;

    void Start()
    {
        timeLeft = totalTime;
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = Mathf.CeilToInt(timeLeft).ToString();
        }
        else
        {
            timerText.text = "0";
            // Handle time out
        }
    }

    public void AddTime(float seconds)
    {
        timeLeft += seconds;
        Debug.Log("Time added: " + seconds + " seconds. New time left: " + timeLeft + " seconds.");
    }
}