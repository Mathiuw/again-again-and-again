using MaiNull.Main_Menu;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MaiNull.UI
{
    public class UIPause : MonoBehaviour
    {
        private bool isPaused = false;

        private void Awake()
        {
            SetPauseState(false);
        }

        private void Update()
        {
            // Listen for Escape key
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                SetPauseState(!isPaused);
            }
        }

        public void SetPauseState(bool state) 
        {
            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(state);
            }

            Time.timeScale = state ? 0f : 1f;

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
}