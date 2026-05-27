using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaiNull
{
    public enum TransitionDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    
    [CreateAssetMenu(fileName = "RoomTransitionData", menuName = "Room/Transition")]
    public class RoomTransitionData : ScriptableObject
    {
        public RoomData desiredRoomData;
        public string entranceId;
        public TransitionDirection spawnDirection;
    }
}