using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [field: SerializeField] Transform RoomCameraTransform;
    [field: SerializeField] Transform PlayerRoomTransition;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && RoomCameraTransform && PlayerRoomTransition)
        {
            CameraMovement cameraMovement = FindFirstObjectByType<CameraMovement>();

            if (cameraMovement)
            {
                cameraMovement.DesiredTransform = RoomCameraTransform;
                collision.transform.position = PlayerRoomTransition.position;
            }
        }
    }
}
