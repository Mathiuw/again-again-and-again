using UnityEngine;
using UnityEngine.UI;

public class OptionsPanelController : MonoBehaviour
{
    public GameObject panel;
    public Slider masterSlider;
    public Slider musicSlider;

    private void Start()
    {
        panel.SetActive(false);

        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

        masterSlider.onValueChanged.AddListener(OnMasterVolumeChange);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChange);
    }

    public void ShowPanel() => panel.SetActive(true);
    public void HidePanel() => panel.SetActive(false);

    private void OnMasterVolumeChange(float value)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMasterVolume(value);
    }

    private void OnMusicVolumeChange(float value)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMusicVolume(value);
    }
}