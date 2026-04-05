using UnityEngine;

public class UI_Pause : MonoBehaviour
{
   private bool isPaused = false;

    private void Awake()
    {
        SetPauseState(false);
    }

    void Update()
    {
        // Listen for Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPauseState(!isPaused);
        }
    }

    public void SetPauseState(bool state) 
    {
        foreach (Transform transform in transform)
        {
            transform.gameObject.SetActive(state);
        }

        if (state)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        isPaused = state;
    }

    public void OpenOptionMenu() 
    {
        OptionsManager.Instance.EnterOptions();
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.SceneTransition(0, Color.black);
    }
}