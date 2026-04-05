using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Gizmos Settings")]
    [SerializeField] private int _roomWidth = 40;
    [SerializeField] private int _roomHeight = 24;
    private Enemy[] enemiesOnTheRoom = new Enemy[0];

    public int EnemyRoomCount
    { 
        get 
        {
            return enemiesOnTheRoom.Length;        
        } 
    }

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

        if (enemiesOnTheRoom.Length > 0)
        {
            foreach (Enemy enemy in enemiesOnTheRoom)
            {
                enemy.health.OnDie += OnEnemyDie;
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
        int enemyCount = 0;

        List<Enemy> enemiesList = new();

        foreach (Transform t in transform)
        {
            if (t.TryGetComponent(out Enemy enemy))
            {
                enemy.health.OnDie += OnEnemyDie;
                enemiesList.Add(enemy);
            }
        }

        if (enemyCount == 0)
        {
            OpenRoomDoors();
        }

        enemiesOnTheRoom = enemiesList.ToArray();
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
        if (transform == cameraPosition)
        {
            SetEnemiesState(true);
        }
        else
        {
            SetEnemiesState(false);
        }
    }

    private void SetEnemiesState(bool state) 
    {
        foreach (Enemy enemy in enemiesOnTheRoom)
        {
            enemy.gameObject.SetActive(state);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.orange;
        Gizmos.DrawWireCube(transform.position, new Vector3(_roomWidth, _roomHeight));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;       
        Gizmos.DrawWireCube(transform.position, new Vector3(_roomWidth, _roomHeight));
    }
}
