using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaiNull
{
    public class RoomTransition : MonoBehaviour
    {
        public static event Action<RoomTransitionData> OnRoomTransition;
        
        [SerializeField] public RoomTransitionData transitionData;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.transform.CompareTag("Player") || !transitionData) return;

            TransitionToRoom(collision.transform);
        }

        private void TransitionToRoom(Transform transitioner)
        {
            if (SceneManager.GetSceneByName(transitionData.desiredRoomData.sceneName).IsValid()) return;
            
            _ = RoomManager.LoadRoom(transitionData.desiredRoomData);
            
            OnRoomTransition?.Invoke(transitionData);
        }
    }
}
