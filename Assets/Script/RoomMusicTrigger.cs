using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomMusicTrigger : MonoBehaviour
{
    public AudioClip roomMusic;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && roomMusic)
        {
            MusicManager.Instance.ChangeMusic(roomMusic);
        }
    }
}