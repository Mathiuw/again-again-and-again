using System;
using System.Collections;
using UnityEngine;

namespace MaiNull
{
    public class RoomTransition : MonoBehaviour
    {
        [SerializeField] public RoomTransitionData transitionData;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.transform.CompareTag("Player") || !transitionData) return;
            
            CameraMovement cameraMovement = FindFirstObjectByType<CameraMovement>();

            if (!cameraMovement) return;
            
            cameraMovement.DesiredTransform = transitionData.roomPrefab.transform;
            // collision.transform.position = DesiredPlayerTransform.position;
        }

        private IEnumerator TransitionToRoom(Transform transitioner)
        {
            if (!transitionData) yield break;
            AsyncInstantiateOperation asyncInstantiateOperation;
            
            yield return asyncInstantiateOperation = InstantiateAsync(transitionData.roomPrefab, null);
            Room newRoom = (Room)asyncInstantiateOperation.Result[0];

            newRoom.transform.position = transitionData.transitionDirection switch
            {
                TransitionDirection.Up => transitioner.position,
                TransitionDirection.Down => transitioner.position,
                TransitionDirection.Left => transitioner.position,
                TransitionDirection.Right => transitioner.position,
                _ => throw new ArgumentOutOfRangeException()
            };

            transitioner.position = newRoom.transform.position;
            
            Debug.Log("New Room Loaded");
        }
    }
}
