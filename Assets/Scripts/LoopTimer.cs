using System;
using UnityEngine;

namespace MaiNull
{
    public class LoopTimer : MonoBehaviour
    {
        [SerializeField] private float timerStartSeconds = 180f;

        public float TimerStartSeconds => timerStartSeconds;
        
        private static float CurrentLoopTimerSeconds { get; set; } = 0f;

        public static event Action<float> OnTimerValueChange;
        public static event Action OnTimerEnd;

        private void Awake()
        {
            CurrentLoopTimerSeconds = timerStartSeconds;
        }

        private void Update()
        {
            RemoveTime(Time.deltaTime);
            OnTimerValueChange?.Invoke(CurrentLoopTimerSeconds);

            if (CurrentLoopTimerSeconds <= 0f)
            {
                EndTimer();
            }
        }

        public void AddTime(float seconds)
        {
            CurrentLoopTimerSeconds += seconds; 
            CurrentLoopTimerSeconds = Mathf.Min(CurrentLoopTimerSeconds, CurrentLoopTimerSeconds);
        }

        public static void RemoveTime(float seconds) 
        {
            CurrentLoopTimerSeconds -= seconds;
            CurrentLoopTimerSeconds = Mathf.Max(0, CurrentLoopTimerSeconds);
        }

        private void EndTimer()
        {
            CurrentLoopTimerSeconds = 0;
            OnTimerEnd?.Invoke();
            enabled = false;
        }
    }
}
