using System;
using UnityEngine;

namespace MaiNull
{
    public class CameraSystem : MonoBehaviour
    {
        public static event Action<Transform> OnCameraTransformUpdate;
        
        [SerializeField] private float lerpSpeed = 3f;

        public static Transform DesiredTransform;

        private void Start()
        {
            Room room = FindFirstObjectByType<Room>();
            
            if (room == null)  return;
            
            DesiredTransform = room.transform;
        }

        private void LateUpdate()
        {
            LerpCamera(DesiredTransform ? DesiredTransform.position : Vector3.zero);
        }

        private void LerpCamera(Vector3 desiredPosition)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, lerpSpeed * Time.deltaTime);
        }
    }
}
