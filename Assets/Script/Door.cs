using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform _DoorTransform;
    [SerializeField] ParticleSystem _particleSystem;

    public void OpenDoor() 
    {
        Destroy(_DoorTransform.gameObject);
        _particleSystem.Play();
    }
}
