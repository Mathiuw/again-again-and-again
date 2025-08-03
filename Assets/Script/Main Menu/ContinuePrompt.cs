using UnityEngine;
using UnityEngine.InputSystem; // For the new Input System
using UnityEngine.SceneManagement;

public class ContinuePrompt : MonoBehaviour
{
    public CanvasGroup promptGroup; // Assign in Inspector
    public ScreenFader screenFader; // Assign in Inspector
    [Tooltip("Name of the next scene to load when player clicks/presses a key")]
    public string nextScene;

    private bool pressed = false;

    void Start()
    {
        promptGroup.alpha = 1;
        if (screenFader != null)
            StartCoroutine(screenFader.FadeIn());
    }

    void Update()
    {
        if (!pressed && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            pressed = true;
            StartCoroutine(ContinueRoutine());
        }
    }

    private System.Collections.IEnumerator ContinueRoutine()
    {
        if (screenFader != null)
            yield return screenFader.FadeOut();
        if (!string.IsNullOrEmpty(nextScene))
            SceneManager.LoadScene(nextScene);
    }
}