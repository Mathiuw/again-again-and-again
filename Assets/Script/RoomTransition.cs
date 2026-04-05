using UnityEngine;

[SelectionBase]
public class RoomTransition : MonoBehaviour
{
    [field: SerializeField] public Transform DesiredRoomTransform { get; private set; }
    [field: SerializeField] public Transform DesiredPlayerTransform { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && DesiredRoomTransform && DesiredPlayerTransform)
        {
            CameraMovement cameraMovement = FindFirstObjectByType<CameraMovement>();

            if (cameraMovement)
            {
                cameraMovement.DesiredTransform = DesiredRoomTransform;
                collision.transform.position = DesiredPlayerTransform.position;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (DesiredPlayerTransform)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(DesiredPlayerTransform.position, 0.5f);
        }

        if (DesiredRoomTransform)
        {
            Gizmos.color = Color.orange;
            Gizmos.DrawSphere(DesiredRoomTransform.position, 1f);
        }
    }
}
