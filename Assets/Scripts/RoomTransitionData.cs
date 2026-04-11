using UnityEngine;

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
        public string transitionId;
        public Room roomPrefab;
        public TransitionDirection transitionDirection;
    }
}