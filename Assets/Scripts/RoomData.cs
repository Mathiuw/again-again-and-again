using UnityEngine;

namespace MaiNull
{
    [CreateAssetMenu(fileName = "Room_Data", menuName = "Room/Data")]
    public class RoomData : ScriptableObject
    {
        public string sceneName;
        public float width;
        public float height;
    }
}