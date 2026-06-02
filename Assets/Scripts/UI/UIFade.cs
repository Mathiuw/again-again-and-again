using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MaiNull.UI
{
    public class UIFade : MonoBehaviour
    {
        public enum EFadeType
        {
            FadeIn, 
            FadeOut,
            Blink,
        }
        
        [SerializeField] private bool activateOnStart = false;
        [SerializeField] private EFadeType eFadeType;
        [SerializeField] private float fadeTime = 1f;
        [SerializeField] private AnimationCurve curve;
        private RawImage _rawImage;

        public float Alpha { get; private set; }

        public event Action OnFadeStart;
        public event Action OnFadeFinish;
        
        public EFadeType FadeType
        {
            get => eFadeType;
            set => eFadeType = value;
        }
        
        private void Awake()
        {
            _rawImage = GetComponentInChildren<RawImage>();
            
            // Initialize initial color
            switch (FadeType)
            {
                case EFadeType.FadeIn:
                    SetImageAlpha(0f);
                    break;
                case EFadeType.FadeOut:
                    SetImageAlpha(1f);
                    break;
                case EFadeType.Blink:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void Start()
        {
            if (!activateOnStart) return;

            switch (FadeType)
            {
                case EFadeType.FadeIn:
                    FadeIn();
                    break;
                case EFadeType.FadeOut:
                    FadeOut();
                    break;
                case EFadeType.Blink:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void FadeActivate()
        {
            switch (FadeType)
            {
                case EFadeType.FadeIn:
                    FadeIn();
                    break;
                case EFadeType.FadeOut:
                    FadeOut();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void ChangeFadeColor(Color color) 
        {
            _rawImage.color = color;
        }

        private void SetImageAlpha(float value)
        {
            Color color = _rawImage.color;
            color.a = value;

            Alpha = color.a;
            _rawImage.color = color;
        }

        private void FadeIn() => StartCoroutine(FadeCoroutine(0, 1));

        public void FadeIn(Color fadeColor) 
        {
            ChangeFadeColor(fadeColor);
            FadeIn();
        }

        private void FadeOut() => StartCoroutine(FadeCoroutine(1, 0));

        public void FadeOut(Color fadeColor) 
        {
            ChangeFadeColor(fadeColor);
            FadeOut();
        }

        public IEnumerator FadeCoroutine(float initial, float final)
        {
            OnFadeStart?.Invoke();
            float timePassed = 0;

            SetImageAlpha(initial);
            while (timePassed < fadeTime)
            {
                SetImageAlpha(curve.Evaluate(Mathf.Lerp(initial, final, timePassed)));
                timePassed += (Time.deltaTime / fadeTime);

                yield return null;
            }
            SetImageAlpha(final);
            OnFadeFinish?.Invoke();
        }

        public IEnumerator FadeAndDestroyCoroutine(float initial, float final)
        {
            yield return FadeCoroutine(initial, final);
            Destroy(gameObject);
        }
        
    }
}