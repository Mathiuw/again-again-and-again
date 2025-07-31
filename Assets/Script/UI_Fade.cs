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
                SetImageAlphaValue(0f);
                break;
            case EFadeType.FadeOut:
                SetImageAlphaValue(1f);
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

    public void SetImageAlphaValue(float value)
    {
        Color color = rawImage.color;
        color.a = value;

        alpha = color.a;
        rawImage.color = color;
    }

    public void FadeIn() => StartCoroutine(FadeCoroutine(0, 1));

    public void FadeOut() => StartCoroutine(FadeCoroutine(1, 0));

    private IEnumerator FadeCoroutine(float initial, float final)
    {
        float timePassed = 0;

        SetImageAlphaValue(initial);
        while (timePassed < fadeTime)
        {
            SetImageAlphaValue(curve.Evaluate(Mathf.Lerp(initial, final, timePassed)));
            timePassed += (Time.deltaTime / fadeTime);

            yield return null;
        }
        SetImageAlphaValue(final);

        yield break;
    }
}
