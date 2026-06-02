using UnityEngine;

namespace MaiNull
{
    public class TopDownCamera : MonoBehaviour
    {
        [SerializeField] private float lerpSpeed = 3f;

        public static Transform Target;

        private void Awake()
        {
            RoomManager.OnCurrentRoomUpdate += OnCurrentRoomUpdate;
        }
        private void OnCurrentRoomUpdate(Room room)
        {
            Target = room.transform;
        }

        private void Start()
        {
            Room room = FindFirstObjectByType<Room>();
            
            if (room == null)  return;
            
            Target = room.transform;
        }

        private void LateUpdate()
        {
            LerpCamera(Target ? Target.position : Vector3.zero);
        }

        private void LerpCamera(Vector3 desiredPosition)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, lerpSpeed * Time.deltaTime);
        }
    }
}
