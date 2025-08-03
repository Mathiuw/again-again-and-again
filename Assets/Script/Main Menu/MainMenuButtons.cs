using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public ScreenFader screenFader; // Assign in Inspector
    public string nextScene = "Introduction"; // Set in Inspector

    public void PlayGame()
    {
        StartCoroutine(PlayRoutine());
    }

    private System.Collections.IEnumerator PlayRoutine()
    {
        yield return screenFader.FadeOut();
        SceneManager.LoadScene(nextScene);
    }

    // ... (Options, Exit methods unchanged)
}