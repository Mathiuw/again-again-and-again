using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [field: SerializeField] public float TimerStartSeconds = 180f;
    public float CurrentTimerSeconds { get; private set; } = 0f;

    public event Action<float> OnTimerValueChange;
    public event Action OnTimerEnd;

    private void Awake()
    {
        CurrentTimerSeconds = TimerStartSeconds;
    }

    private void Update()
    {
        CurrentTimerSeconds -= Time.deltaTime;
        OnTimerValueChange?.Invoke(CurrentTimerSeconds);

        if (CurrentTimerSeconds <= 0f)
        {
            OnTimerEnd?.Invoke();
            enabled = false;
        }
    }

    public void AddTime(float seconds)
    {
        TimerStartSeconds += seconds; // Or whatever your timer variable is called
    }

}
