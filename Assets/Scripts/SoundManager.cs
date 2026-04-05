using UnityEngine;

public enum ESoundType 
{
    SHOOT,
    HIT,
    DOOR,
    ENEMY,
    UI,
    MEDUSASHOOT,
    BOWAUDIO
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClip[] _soundList;
    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(ESoundType sound, float volume = 1) 
    {
        Instance._audioSource.PlayOneShot(Instance._soundList[(int)sound], volume);
    }
}
