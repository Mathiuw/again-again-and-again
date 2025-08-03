using UnityEngine;
using TMPro; // Or using UnityEngine.UI if using legacy Text

public class BlinkingText : MonoBehaviour
{
    public TextMeshProUGUI promptText; // Assign in Inspector
    public float blinkSpeed = 1.0f;    // How fast to blink (cycles per second)

    void Start()
    {
        if (promptText == null) promptText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Blink());
    }

    System.Collections.IEnumerator Blink()
    {
        while (true)
        {
            float t = (Mathf.Sin(Time.unscaledTime * Mathf.PI * blinkSpeed) + 1f) / 2f; // 0..1
            var color = promptText.color;
            color.a = t;
            promptText.color = color;
            yield return null;
        }
    }
}