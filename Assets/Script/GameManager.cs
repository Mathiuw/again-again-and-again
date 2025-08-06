using System;
using System.Collections;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] UI_Fade fade;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Timer timer = FindFirstObjectByType<Timer>();
        if (timer)
        {
            timer.OnTimerEnd += OnTimerEnd;
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player)
        {
            Health health = player.GetComponent<Health>();

            if (health)
            {
                health.OnDie += OnPlayerDie;
            }
        }
    }

    private void OnTimerEnd()
    {
        StartCoroutine(SceneTransitionCoroutine(SceneManager.GetActiveScene().buildIndex, Color.white));
        AddRestartCount();
    }

    private void OnPlayerDie()
    {
        Timer timer = FindFirstObjectByType<Timer>();
        timer.OnTimerEnd -= OnTimerEnd;

        StartCoroutine(SceneTransitionCoroutine(SceneManager.GetActiveScene().buildIndex, Color.white));
        AddRestartCount();
    }

    public void SceneTransition(int sceneIndex, Color fadeColor) 
    {
        StartCoroutine(SceneTransitionCoroutine(sceneIndex, fadeColor));
    }

    private IEnumerator SceneTransitionCoroutine(int sceneIndex, Color fadeColor) 
    {
        UI_Fade fade = Instantiate(this.fade);
        fade.FadeIn(fadeColor);

        while (fade.alpha < 1)
        {
            yield return null;
        }

        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);

        yield break;
    }

    private void AddRestartCount() 
    {
        // Adds or set restart count on playerprefs
        // In case needs to reset count: go to %userprofile%\AppData\Local\Packages\[ProductPackageId]\LocalState\playerprefs.dat and delete the file
        if (PlayerPrefs.GetInt("RestartCount") == 0)
        {
            PlayerPrefs.SetInt("RestartCount", 1);
        }
        else
        {
            int newRestartCountValue = PlayerPrefs.GetInt("RestartCount") + 1;

            PlayerPrefs.SetInt("RestartCount", newRestartCountValue);
        }
    }
}
