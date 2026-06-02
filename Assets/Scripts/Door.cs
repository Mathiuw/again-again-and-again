using UnityEngine;

namespace MaiNull
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private GameObject doorSprite;
        [SerializeField] private ParticleSystem doorParticleSystem;

        public void OpenDoor() 
        {
            doorSprite.SetActive(false);
            doorParticleSystem.Play();
        }

        public void CloseDoor()
        {
            doorSprite.SetActive(true);
            doorParticleSystem.Stop();
        }
    }
}
