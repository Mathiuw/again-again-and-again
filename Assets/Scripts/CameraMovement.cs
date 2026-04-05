using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _desiredTransform;

    public Transform DesiredTransform 
    { 
        get 
        { 
            return _desiredTransform; 
        } 
        set 
        { 
            _desiredTransform = value;
            OnCameraTransformUpdate?.Invoke(value);
        } 
    }

    [SerializeField] private float _lerpSpeed = 3f;

    public event Action<Transform> OnCameraTransformUpdate;

    private void Start()
    {
        if (_desiredTransform)
        {
            transform.position = _desiredTransform.position;
        }
    }

    private void LateUpdate()
    {
        if (DesiredTransform)
        {
            LerpCamera(DesiredTransform.position);
        }
        else
        {
            LerpCamera(Vector3.zero);
        }
    }

    private void LerpCamera(Vector3 desiredPosition)
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _lerpSpeed * Time.deltaTime);
    }
}
