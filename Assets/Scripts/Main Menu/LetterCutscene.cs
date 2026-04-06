using UnityEngine;
using UnityEngine.InputSystem;

namespace MaiNull.Main_Menu
{
    public class LetterCutscene : MonoBehaviour
    {
        private void Update()
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                GameManager.Instance.SceneTransition(2, Color.black);
                Destroy(this);
            }
        }
    }
}