using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage; // Assign in Inspector: a UI Image that covers the screen
    public float fadeDuration = 1.0f;

    void Awake()
    {
        // Ensure the image starts transparent
        SetAlpha(0f);
    }

    public void SetAlpha(float alpha)
    {
        var color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }

    public IEnumerator FadeOut()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(Mathf.Lerp(0, 1, t / fadeDuration));
            yield return null;
        }
        SetAlpha(1);
    }

    public IEnumerator FadeIn()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(Mathf.Lerp(1, 0, t / fadeDuration));
            yield return null;
        }
        SetAlpha(0);
    }
}