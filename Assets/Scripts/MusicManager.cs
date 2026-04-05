using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    protected AudioSource _audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _audioSource = GetComponent<AudioSource>();

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
        if (_audioSource != null)
            _audioSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void ChangeMusic(AudioClip newClip)
    {
        if (_audioSource.clip == newClip) return; // Already playing this music

        _audioSource.Stop();
        _audioSource.clip = newClip;
        _audioSource.Play();
    }
}