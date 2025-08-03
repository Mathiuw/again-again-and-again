using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume", 1f));
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1f));
    }

    public void SetMasterVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        if (musicSource != null)
            musicSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
}