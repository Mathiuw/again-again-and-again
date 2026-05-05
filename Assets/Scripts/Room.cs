using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MaiNull
{
    [Serializable]
    public struct RoomTransitionItem
    {
        public string transitionId;
        [FormerlySerializedAs("transitionGameObject")] public Transform transitionTransform;

        public RoomTransitionItem(string transitionId, Transform transitionTransform)
        {
            this.transitionId = transitionId;
            this.transitionTransform = transitionTransform;
        }
    }
    
    public class Room : MonoBehaviour
    {
        [FormerlySerializedAs("_roomWidth")] [SerializeField] private int roomWidth = 40;
        [FormerlySerializedAs("_roomHeight")] [SerializeField] private int roomHeight = 24;
        [SerializeField] private RoomTransitionItem[] roomTransitions;
        
        private Enemy[] _enemiesOnTheRoom = Array.Empty<Enemy>();
        
        public int EnemyRoomCount => _enemiesOnTheRoom.Length;

        private void OnEnable()
        {
            CameraMovement cameraMovement = FindFirstObjectByType<CameraMovement>();
            cameraMovement.OnCameraTransformUpdate += OnCameraTransformUpdate;
        }

        private void OnDisable()
        {
            CameraMovement cameraMovement = FindFirstObjectByType<CameraMovement>();
            if (cameraMovement)
            {
                cameraMovement.OnCameraTransformUpdate -= OnCameraTransformUpdate;
            }
        }

        private void Start()
        {
            CountEnemies();

            if (_enemiesOnTheRoom.Length > 0)
            {
                foreach (Enemy enemy in _enemiesOnTheRoom)
                {
                    enemy.Health.OnDie += OnEnemyDie;
                }
            }

            SetEnemiesState(false);
        }

        private void OnEnemyDie()
        {
            CountEnemies();
        }

        private void CountEnemies() 
        {
            List<Enemy> enemiesList = new();

            foreach (Transform t in transform)
            {
                if (!t.TryGetComponent(out Enemy enemy)) continue;
                
                enemy.Health.OnDie += OnEnemyDie;
                enemiesList.Add(enemy);
            }

            if (enemiesList.Count == 0)
            {
                OpenRoomDoors();
            }

            _enemiesOnTheRoom = enemiesList.ToArray();
        }

        private void OpenRoomDoors()
        {
            foreach (Door door in GetComponentsInChildren<Door>())
            {
                door.OpenDoor();
            }
        }

        private void OnCameraTransformUpdate(Transform cameraPosition)
        {
            SetEnemiesState(transform == cameraPosition);
        }

        private void SetEnemiesState(bool state) 
        {
            foreach (Enemy enemy in _enemiesOnTheRoom)
            {
                enemy.gameObject.SetActive(state);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.orange;
            Gizmos.DrawWireCube(transform.position, new Vector3(roomWidth, roomHeight));

            foreach (RoomTransitionItem item in roomTransitions)
            {
                if (item.transitionTransform)
                {
                    Gizmos.DrawSphere(transform.position, 3);
                }
            }
        }
    }
}
