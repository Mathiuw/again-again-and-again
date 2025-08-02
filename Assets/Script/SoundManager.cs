using UnityEngine;

public enum ESoundType 
{
    SHOOT,
    HIT,
    DOOR,
    ENEMY,
    UI
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClip[] soundList;
    private AudioSource audioSource;

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

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(ESoundType sound, float volume = 1) 
    {
        Instance.audioSource.PlayOneShot(Instance.soundList[(int)sound], volume);
    }
}
