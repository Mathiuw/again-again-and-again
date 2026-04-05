using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timerStartSeconds = 180f;

    public float CurrentTimerSeconds { get; private set; } = 0f;

    public event Action<float> OnTimerValueChange;
    public event Action OnTimerEnd;

    private void Awake()
    {
        CurrentTimerSeconds = timerStartSeconds;
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
        CurrentTimerSeconds += seconds; 
        CurrentTimerSeconds = Mathf.Min(CurrentTimerSeconds, CurrentTimerSeconds);
    }

    public void RemoveTime(float seconds) 
    {
        CurrentTimerSeconds -= seconds;
        CurrentTimerSeconds = Mathf.Max(0, CurrentTimerSeconds);
    }
}
