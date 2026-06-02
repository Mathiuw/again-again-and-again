using System;
using UnityEditor;
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
        [SerializeField] private RoomData data;
        [SerializeField] private RoomTransitionItem[] roomTransitions;
        private Door[] _doors;
        
        public RoomData Data => data;

        private void Awake()
        {
            _doors = GetComponentsInChildren<Door>();
        }

        private void Start()
        {
            foreach (Enemy enemy in Enemy.EnemiesList) {
                enemy.Health.OnDie += OnEnemyDie;
            }
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
            foreach (Door door in _doors)
            {
                door.OpenDoor();
            }
        }
        
        public void SnapLeft(Room room)
        {
            transform.position = new Vector2(room.transform.position.x - room.data.width / 2 - data.width / 2, room.transform.position.y);
        }
        
        public void SnapRight(Room room)
        {
            transform.position = new Vector2(room.transform.position.x + room.data.width / 2 + data.width / 2, room.transform.position.y);
        }

        public void SnapUp (Room room)
        {
            transform.position = new Vector2(room.transform.position.x, room.transform.position.y + room.data.height / 2 + data.height / 2);
        }

        public void SnapDown (Room room)
        {
            transform.position = new Vector2(room.transform.position.x, room.transform.position.y - room.data.height / 2 - data.height / 2);
        }
        
        private void OnDrawGizmos()
        {
            if (!Data) return;
            
            Gizmos.color = Color.orange;
            Gizmos.DrawWireCube(transform.position, new Vector3(data.width, data.height));
            
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
