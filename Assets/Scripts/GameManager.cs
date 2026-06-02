using MaiNull.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaiNull
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private UIFade fade;

        private void Start()
        {
            LoopTimer.OnLoopEnd += OnLoopEnd;
            Player.OnPlayerDie += OnPlayerDie;
        }

        private void OnLoopEnd()
        {
            SceneTransition(SceneManager.GetActiveScene().buildIndex, Color.white);
            AddRestartCount();
        }

        private void OnPlayerDie()
        {
            LoopTimer.OnLoopEnd -= OnLoopEnd;
            OnLoopEnd();
        }

        public void SceneTransition(int sceneIndex, Color fadeColor) 
        {
            if (!fade)
            {
                SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
                return;
            }
            
            UIFade newFade = Instantiate(this.fade);
            newFade.FadeIn(fadeColor);

            newFade.OnFadeFinish += () =>
            {
                SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
            };
        }

        private static void AddRestartCount() 
        {
            // Adds or set restart count on player prefs
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
