using UnityEngine;

namespace MaiNull
{
    [CreateAssetMenu(fileName = "Room_Data", menuName = "Room/Data")]
    public class RoomData : ScriptableObject
    {
        public string sceneName;
        public int width = 40;
        public int height = 24;
    }
}