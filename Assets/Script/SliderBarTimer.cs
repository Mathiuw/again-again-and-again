using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SliderBarTimer : MonoBehaviour
{
    private Slider SliderBar;
    [SerializeField] private Color Color;
    void Start()
    {
       Timer timer = FindFirstObjectByType<Timer>();
        SliderBar.maxValue = 180f;
        SliderBar.value = timer._currentTimer;
    }


    private void Awake()
    {
        SliderBar = GetComponent<Slider>();
        if (SliderBar == null)
        {
            Debug.LogError("SliderBar component not found on this GameObject.");
        }
    }


    private void Update()
    {
        SliderBar.value -= Time.deltaTime;
        if (SliderBar.value <= 170f) 
            {
            GetComponentInChildren<Image>().color = Color;
        }
    }
}