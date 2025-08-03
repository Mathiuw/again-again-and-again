using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroductionController : MonoBehaviour
{
    public CanvasGroup pressAnyKeyGroup;
    public float lerpDuration = 1.0f;
    private bool pressed = false;

    void Start()
    {
   
        // Show "Press Anything"
        pressAnyKeyGroup.alpha = 1;
    }

    void Update()
    {
        if (!pressed && Input.anyKeyDown)
        {
            pressed = true;
          
        }
    }

    System.Collections.IEnumerator ShowButtons()
    {
        // Fade out "Press Anything"
        float t = 0;
        while (t < lerpDuration)
        {
            t += Time.deltaTime;
            pressAnyKeyGroup.alpha = Mathf.Lerp(1, 0, t / lerpDuration);
            yield return null;
        }
        pressAnyKeyGroup.alpha = 0;

        // Optional: Deactivate the group after fade-out
        pressAnyKeyGroup.interactable = false;
        pressAnyKeyGroup.blocksRaycasts = false;

        // Fade in buttons
        t = 0;
        while (t < lerpDuration)
        {
            t += Time.deltaTime;
            
            yield return null;
        }
        
    }
}

