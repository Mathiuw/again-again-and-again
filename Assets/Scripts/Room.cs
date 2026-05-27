using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace MaiNull
{
    [Serializable]
    public struct RoomTransitionItem
    {
        public string id;
        public Transform transform;

        public RoomTransitionItem(string id, Transform transform)
        {
            this.id = id;
            this.transform = transform;
        }
    }
    
    public class Room : MonoBehaviour
    {
        [SerializeField] private RoomData roomData;
        [SerializeField] private RoomTransitionItem[] roomTransitions;

        private void Start()
        {
            CheckEnemyCount();
        }

        private void Update()
        {
            CheckEnemyCount();
        }

        private void OnEnemyDie()
        {
            CheckEnemyCount();
        }

        private int CheckEnemyCount() 
        {
            if (Enemy.EnemiesList.Count == 0)
            {
                OpenRoomDoors();
            }

            return Enemy.EnemiesList.Count;
        }

        private void OpenRoomDoors()
        {
            foreach (Door door in FindObjectsByType<Door>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
            {
                door.OpenDoor();
            }
        }

        private void OnDrawGizmos()
        {
            if (!roomData) return;
            
            Gizmos.color = Color.orange;
            Gizmos.DrawWireCube(transform.position, new Vector3(roomData.width, roomData.height));

            if (roomTransitions.Length <= 0) return;

            Gizmos.color = Color.red;
            
            foreach (RoomTransitionItem transition in roomTransitions)
            {
                if (!transition.transform) continue;
                
                Gizmos.DrawSphere(transition.transform.position, 0.25f);
            }

        }
    }
}
