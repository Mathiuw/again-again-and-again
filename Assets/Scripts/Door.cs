using UnityEngine;

namespace MaiNull
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Transform doorTransform;
        [SerializeField] private ParticleSystem doorParticleSystem;

        public void OpenDoor() 
        {
            Destroy(doorTransform.gameObject);
            doorParticleSystem.Play();
        }
    }
}
