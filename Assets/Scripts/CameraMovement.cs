using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace MaiNull
{
    public class CameraMovement : MonoBehaviour
    {
        [FormerlySerializedAs("_desiredTransform")] [SerializeField] private Transform desiredTransform;
        [FormerlySerializedAs("_lerpSpeed")] [SerializeField] private float lerpSpeed = 3f;
        
        public Transform DesiredTransform 
        { 
            get => desiredTransform;
            set 
            { 
                desiredTransform = value;
                OnCameraTransformUpdate?.Invoke(value);
            } 
        }

        public event Action<Transform> OnCameraTransformUpdate;

        private void Start()
        {
            if (desiredTransform)
            {
                transform.position = desiredTransform.position;
            }
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
