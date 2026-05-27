using UnityEngine;

namespace MaiNull
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Transform doorTransform;
        [SerializeField] private ParticleSystem doorParticleSystem;

        public void OpenDoor() 
        {
            doorTransform.gameObject.SetActive(true);
            doorParticleSystem.Play();
        }

        public void CloseDoor()
        {
            doorTransform.gameObject.SetActive(false);
            doorParticleSystem.Stop();
        }
    }
}
