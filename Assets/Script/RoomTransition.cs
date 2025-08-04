using UnityEngine;

[SelectionBase]
public class RoomTransition : MonoBehaviour
{
    [field: SerializeField] public Transform DesiredCameraTransform { get; private set; }
    [field: SerializeField] public Transform RoomTransitionPosition { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && DesiredCameraTransform && RoomTransitionPosition)
        {
            CameraMovement cameraMovement = FindFirstObjectByType<CameraMovement>();

            if (cameraMovement)
            {
                cameraMovement.DesiredTransform = DesiredCameraTransform;
                collision.transform.position = RoomTransitionPosition.position;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (RoomTransitionPosition)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(RoomTransitionPosition.position, 0.5f);
        }

        if (DesiredCameraTransform)
        {
            Gizmos.color = Color.orange;
            Gizmos.DrawSphere(DesiredCameraTransform.position, 1f);
        }
    }
}
