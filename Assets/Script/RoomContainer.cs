using System;
using UnityEngine;

public class RoomContainer : MonoBehaviour
{
    [SerializeField] private int _roomWidth = 40;
    [SerializeField] private int _roomHeight = 24;
    [SerializeField] private Transform cameraPosition;

    private void Awake()
    {
        foreach (Transform t in GetComponents<Transform>()) 
        {
            if (t.CompareTag("Camera Position"))
            {
                cameraPosition = t;
                break;
            }
        }
    }

    private void Start()
    {
        CameraMovement cameraMovement = FindFirstObjectByType<CameraMovement>();
        if (cameraMovement)
        {
            cameraMovement.OnCameraTransformUpdate += OnCameraTransformUpdate;
        }

        foreach (Transform t in transform)
        {
            if (t.CompareTag("Enemy"))
            {
                Health health = t.GetComponent<Health>();
                if (health)
                {
                    health.OnDie += OnEnemyDie;
                }
            }
        }

        OnEnemyDie();
    }

    private void OnEnemyDie()
    {
        int enemyCount = 0;

        foreach (Transform t in transform)
        {
            if (t.CompareTag("Enemy"))
            {
                Health health = t.GetComponent<Health>();

                if (!health.IsDead)
                {
                    enemyCount++;
                }           
            }
        }

        if (enemyCount == 0)
        {
            OpenRoomDoors();
        }
    }

    private void OpenRoomDoors()
    {
        foreach (Door door in GetComponentsInChildren<Door>())
        {
            door.OpenDoor();
        }
    }

    private void OnDisable()
    {
        CameraMovement cameraMovement = FindFirstObjectByType<CameraMovement>();
        if (cameraMovement)
        {
            cameraMovement.OnCameraTransformUpdate -= OnCameraTransformUpdate;
        }
    }

    private void OnCameraTransformUpdate(Transform cameraPosition)
    {
        if (this.cameraPosition == cameraPosition)
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
        foreach (Transform t in transform)
        {
            if (t.CompareTag("Enemy"))
            {
                t.gameObject.SetActive(state);
            }
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
