using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] UI_Fade fade;

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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);

        yield break;
    }
}
