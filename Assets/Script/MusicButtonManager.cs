using UnityEngine;

public class MusicButtonManager : MonoBehaviour
{
    public AudioSource[] musicSources;

    public void PlayMusic(int index)
    {
        // Stop all music
        for (int i = 0; i < musicSources.Length; i++)
        {
            if (musicSources[i].isPlaying)
                musicSources[i].Stop();
        }

        // Play the selected music
        if (index >= 0 && index < musicSources.Length)
        {
            musicSources[index].Play();
        }
    }
}