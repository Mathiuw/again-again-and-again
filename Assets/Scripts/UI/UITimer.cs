using System;
using UnityEngine;
using UnityEngine.UI;

namespace MaiNull.UI
{
    public class UITimer : MonoBehaviour
    {
        [SerializeField] private Slider sliderBar;

        private void Awake()
        {
            if (!sliderBar)
            {
                Debug.LogError("SliderBar component not found on this GameObject.");
                return;
            }
        }

        private void OnEnable()
        {
            LoopTimer.OnTimerValueChange += OnTimerValueChange;
        }

        private void Start()
        {
            LoopTimer loopTimer = FindFirstObjectByType<LoopTimer>();
            if (!loopTimer) return;
            
            sliderBar.maxValue = loopTimer.TimerStartSeconds;
            sliderBar.value = loopTimer.TimerStartSeconds;
            
        }

        private void OnDisable()
        {
            LoopTimer.OnTimerValueChange -= OnTimerValueChange;
        }

        private void OnTimerValueChange(float currentSeconds)
        {
            sliderBar.value = currentSeconds;
        }
    }
}
