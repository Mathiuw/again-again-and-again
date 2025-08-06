using System;
using UnityEngine;

public class MenuStart : MonoBehaviour
{
    [SerializeField] private Transform _pressAnyButtom;
    [SerializeField] private CanvasGroup _buttomCanvasGroup;
    [SerializeField] private float _lerpDuration = 1.0f;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(ShowButtons());
        }
    }

    System.Collections.IEnumerator ShowButtons()
    {
        enabled = false;

        Destroy(_pressAnyButtom.gameObject);

        // Fade in menu buttoms
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += _lerpDuration * Time.deltaTime;
            _buttomCanvasGroup.alpha = alpha;
            yield return null;
        }

        Destroy(this);
        yield break;
    }
}

