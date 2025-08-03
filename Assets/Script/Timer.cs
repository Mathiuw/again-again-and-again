using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timerSeconds = 180f;
    public float _currentTimer { get; private set; } = 0f;

    public event Action<float> OnTimerUpdate;
    public event Action OnTimerEnd;

    private void Awake()
    {
        _currentTimer = _timerSeconds;
    }

    private void Update()
    {
        _currentTimer -= Time.deltaTime;
        OnTimerUpdate?.Invoke(_currentTimer);

        if (_currentTimer <= 0f)
        {
            OnTimerEnd?.Invoke();
            enabled = false;
        }
    }

    public void AddTime(float seconds)
    {
        _timerSeconds += seconds; // Or whatever your timer variable is called
    }

}
