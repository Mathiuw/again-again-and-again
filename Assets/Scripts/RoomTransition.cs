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
            
            StartCoroutine(TransitionToRoom(collision.transform));
        }

        private IEnumerator TransitionToRoom(Transform transitioner)
        {
            yield return SceneManager.LoadSceneAsync(transitionData.desiredRoomData.sceneName, LoadSceneMode.Additive);
            
            print("Scene Loaded!");
            OnRoomTransition?.Invoke(transitionData);
        }
    }
}
