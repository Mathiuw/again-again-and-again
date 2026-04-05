using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum EFadeType
{
    FadeIn, FadeOut
}

public class UI_Fade : MonoBehaviour
{
    [SerializeField] bool activateOnStart = false;
    [field: SerializeField] EFadeType EFadeType { get; set; } = EFadeType.FadeIn;
    [SerializeField] float fadeTime = 1f;
    [SerializeField] AnimationCurve curve;
    RawImage rawImage;

    public float alpha { get; private set; }

    private void Start()
    {
        switch (EFadeType)
        {
            case EFadeType.FadeIn:
                SetImageAlpha(0f);
                break;
            case EFadeType.FadeOut:
                SetImageAlpha(1f);
                break;
            default:
                break;
        }

        if (!activateOnStart) return;

        switch (EFadeType)
        {
            case EFadeType.FadeIn:
                FadeIn();
                break;
            case EFadeType.FadeOut:
                FadeOut();
                break;
            default:
                break;
        }
    }

    void Awake()
    {
        rawImage = GetComponentInChildren<RawImage>();
    }

    public void ChangeFadeColor(Color color) 
    {
        rawImage.color = color;
    }

    public void SetImageAlpha(float value)
    {
        Color color = rawImage.color;
        color.a = value;

        alpha = color.a;
        rawImage.color = color;
    }

    public void FadeIn() => StartCoroutine(FadeCoroutine(0, 1));

    public void FadeIn(Color fadeColor) 
    {
        ChangeFadeColor(fadeColor);
        FadeIn();
    }

    public void FadeOut() => StartCoroutine(FadeCoroutine(1, 0));

    public void FadeOut(Color fadeColor) 
    {
        ChangeFadeColor(fadeColor);
        FadeOut();
    }

    private IEnumerator FadeCoroutine(float initial, float final)
    {
        float timePassed = 0;

        SetImageAlpha(initial);
        while (timePassed < fadeTime)
        {
            SetImageAlpha(curve.Evaluate(Mathf.Lerp(initial, final, timePassed)));
            timePassed += (Time.deltaTime / fadeTime);

            yield return null;
        }
        SetImageAlpha(final);

        yield break;
    }
}
