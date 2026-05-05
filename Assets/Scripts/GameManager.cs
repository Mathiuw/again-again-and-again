using System.Collections;
using MaiNull.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaiNull
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private UIFade fade;

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
            LoopTimer.OnLoopEnd += OnLoopEnd;
            Player.OnPlayerDie += OnPlayerDie;
        }

        private void OnLoopEnd()
        {
            StartCoroutine(SceneTransitionCoroutine(SceneManager.GetActiveScene().buildIndex, Color.white));
            AddRestartCount();
        }

        private void OnPlayerDie()
        {
            LoopTimer.OnLoopEnd -= OnLoopEnd;
            OnLoopEnd();
        }

        public void SceneTransition(int sceneIndex, Color fadeColor) 
        {
            StartCoroutine(SceneTransitionCoroutine(sceneIndex, fadeColor));
        }

        private IEnumerator SceneTransitionCoroutine(int sceneIndex, Color fadeColor) 
        {
            UIFade fade = Instantiate(this.fade);
            fade.FadeIn(fadeColor);

            while (fade.alpha < 1)
            {
                yield return null;
            }

            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);

            yield break;
        }

        private static void AddRestartCount() 
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
}
