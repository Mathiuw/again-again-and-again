using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuPanel; // Assign in Inspector

    private bool isPaused = false;

    void Update()
    {
        // Listen for Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; // Pauses the game
        isPaused = true;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        // Replace "MainMenu" with your actual main menu scene name
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}