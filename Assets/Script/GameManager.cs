using System;
using System.Collections;
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
        timer.OnTimerEnd += OnTimerEnd;

        Transform player = GameObject.FindWithTag("Player").transform;
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
        StartCoroutine(RestartLoop());
    }

    private void OnPlayerDie()
    {
        Timer timer = FindFirstObjectByType<Timer>();
        timer.OnTimerEnd -= OnTimerEnd;

        StartCoroutine(RestartLoop());
    }

    private IEnumerator RestartLoop() 
    {
        UI_Fade fade = Instantiate(this.fade);
        fade.FadeIn();

        while (fade.alpha < 1)
        {
            yield return null;
        }

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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);

        yield break;
    }
}
