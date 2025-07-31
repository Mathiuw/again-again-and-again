using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UI_Timer : MonoBehaviour
{
    Timer _timer;
    TextMeshProUGUI _timerText;
    [SerializeField] string _timerPrefix = "Timer: ";

    private void Awake()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _timer = FindFirstObjectByType<Timer>();

        if (_timer)
        {
            _timer.OnTimerUpdate += OnTimerUpdate;
        }
    }

    private void OnDisable()
    {
        _timer.OnTimerUpdate -= OnTimerUpdate;
    }

    private void OnTimerUpdate(float timerValue)
    {
        int minutes = Mathf.FloorToInt(timerValue / 60);
        int seconds = Mathf.FloorToInt(timerValue % 60);
        minutes = Mathf.Clamp(minutes, 0, 60);
        seconds = Mathf.Clamp(seconds, 0, 60);

        _timerText.text = _timerPrefix + string.Format("{00:00}:{1:00}", minutes, seconds);
    }
}
