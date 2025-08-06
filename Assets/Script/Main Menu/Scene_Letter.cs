using UnityEngine;
using UnityEngine.InputSystem; // For the new Input System

public class Scene_Letter : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            GameManager.Instance.SceneTransition(2, Color.black);
            Destroy(this);
        }
    }
}