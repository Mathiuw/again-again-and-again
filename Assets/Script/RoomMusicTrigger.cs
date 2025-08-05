using UnityEngine;

public class RoomMusicTrigger : MonoBehaviour
{
    public AudioClip roomMusic;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && roomMusic != null)
        {
            AudioManager.Instance.ChangeMusic(roomMusic);
        }
    }
}