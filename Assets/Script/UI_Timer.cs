using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UI_Timer : MonoBehaviour
{
    private Slider _sliderBar;

    private void Awake()
    {
        _sliderBar = GetComponent<Slider>();

        if (!_sliderBar)
        {
            Debug.LogError("SliderBar component not found on this GameObject.");
        }
    }

    void Start()
    {
        Timer timer = FindFirstObjectByType<Timer>();

        if (timer)
        {
            _sliderBar.maxValue = timer.TimerStartSeconds;
            _sliderBar.value = timer.TimerStartSeconds;
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
        _sliderBar.value = currentSeconds;
    }
}
