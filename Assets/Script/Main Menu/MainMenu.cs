using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup pressAnyKeyGroup;
    public CanvasGroup buttonsGroup;
    public float lerpDuration = 1.0f;
    private bool pressed = false;

    void Awake()
    {
        // Hide menu buttons
        buttonsGroup.alpha = 0;
        buttonsGroup.interactable = false;
        buttonsGroup.blocksRaycasts = false;
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
        float t = 0;

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

    public void PlayGame()
    {
        GameManager.Instance.SceneTransition(1, Color.black);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
