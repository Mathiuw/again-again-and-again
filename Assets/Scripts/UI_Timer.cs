using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UI_Timer : MonoBehaviour
{
    private Slider sliderBar;

    private void Awake()
    {
        sliderBar = GetComponent<Slider>();

        if (!sliderBar)
        {
            Debug.LogError("SliderBar component not found on this GameObject.");
        }
    }

    void Start()
    {
        Timer timer = FindFirstObjectByType<Timer>();

        if (timer)
        {
            sliderBar.maxValue = timer.TimerStartSeconds;
            sliderBar.value = timer.TimerStartSeconds;
            timer.OnTimerValueChange += OnTimerValueChange;
        }
    }

    private void OnDisable()
    {
        Timer timer = FindFirstObjectByType<Timer>();

        if (timer)
        {
            timer.OnTimerValueChange -= OnTimerValueChange;
        }
    }

    private void OnTimerValueChange(float currentSeconds)
    {
        sliderBar.value = currentSeconds;
    }
}
