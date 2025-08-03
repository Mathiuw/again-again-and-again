using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public CanvasGroup pressAnyKeyGroup;
    public CanvasGroup buttonsGroup;
    public float lerpDuration = 1.0f;
    private bool pressed = false;

    void Start()
    {
        // Hide buttons
        buttonsGroup.alpha = 0;
        buttonsGroup.interactable = false;
        buttonsGroup.blocksRaycasts = false;
        // Show "Press Anything"
        pressAnyKeyGroup.alpha = 1;
    }

    void Update()
    {
        if (!pressed && Input.anyKeyDown)
        {
            pressed = true;
            StartCoroutine(ShowButtons());
        }
    }

    System.Collections.IEnumerator ShowButtons()
    {
        // Fade out "Press Anything"
        float t = 0;
        while (t < lerpDuration)
        {
            t += Time.deltaTime;
            pressAnyKeyGroup.alpha = Mathf.Lerp(1, 0, t / lerpDuration);
            yield return null;
        }
        pressAnyKeyGroup.alpha = 0;

        // Optional: Deactivate the group after fade-out
        pressAnyKeyGroup.interactable = false;
        pressAnyKeyGroup.blocksRaycasts = false;

        // Fade in buttons
        t = 0;
        while (t < lerpDuration)
        {
            t += Time.deltaTime;
            buttonsGroup.alpha = Mathf.Lerp(0, 1, t / lerpDuration);
            yield return null;
        }
        buttonsGroup.alpha = 1;
        buttonsGroup.interactable = true;
        buttonsGroup.blocksRaycasts = true;
    }
}
