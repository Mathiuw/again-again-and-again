using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class UI_Blink : MonoBehaviour
{
    [SerializeField] private float _blinkSpeed = 1.0f;    // How fast to blink (cycles per second)
    CanvasGroup _canvasGroup;

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            float alpha = (Mathf.Sin(Time.unscaledTime * Mathf.PI * _blinkSpeed) + 1f) / 2f; // 0..1 interpolation
            _canvasGroup.alpha = alpha;
            yield return null;
        }
    }
}