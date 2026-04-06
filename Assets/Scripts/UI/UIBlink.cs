using System.Collections;
using UnityEngine;

namespace MaiNull.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIBlink : MonoBehaviour
    {
        [SerializeField] private float blinkSpeed = 1.0f;    // How fast to blink (cycles per second)
        private CanvasGroup canvasGroup;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            StartCoroutine(Blink());
        }

        private IEnumerator Blink()
        {
            while (true)
            {
                float alpha = (Mathf.Sin(Time.unscaledTime * Mathf.PI * blinkSpeed) + 1f) / 2f; // 0..1 interpolation
                canvasGroup.alpha = alpha;
                yield return null;
            }
        }
    }
}