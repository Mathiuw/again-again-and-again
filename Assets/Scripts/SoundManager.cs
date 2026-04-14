using UnityEngine;
using UnityEngine.Serialization;

namespace MaiNull
{
    public enum ESoundType 
    {
        Shoot,
        Hit,
        Door,
        Enemy,
        UI,
        Medusashoot,
        Bowaudio
    }

    // [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        [FormerlySerializedAs("_soundList")] [SerializeField] private AudioClip[] soundList;
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
            if (!Instance) return;
            
            Instance.audioSource.PlayOneShot(Instance.soundList[(int)sound], volume);
        }
    }
}