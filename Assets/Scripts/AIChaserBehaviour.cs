using Pathfinding;
using UnityEngine;

namespace MaiNull
{
    public class AIChaserBehaviour : MonoBehaviour
    {
        [Header("AI Chaser Settings")]
        public Transform target;
        private IAstarAI _ai;
        private bool _active = true;

        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                if (!value) _ai.onSearchPath -= Update;
                else
                {
                    _ai.onSearchPath += Update;
                }
            } 
        }

        private void OnEnable () {
            _ai = GetComponent<IAstarAI>();
            // Update the destination right before searching for a path as well.
            // This is enough in theory, but this script will also update the destination every
            // frame as the destination is used for debugging and may be used for other things by other
            // scripts as well. So it makes sense that it is up to date every frame.
            if (_ai != null) _ai.onSearchPath += Update;
        }

        private void Start()
        {
            Transform playerTransform = FindFirstObjectByType<Player>()?.transform;
            if (playerTransform)
            {
                target = playerTransform;
            }
        }
        
        private void OnDisable () {
            if (_ai != null) _ai.onSearchPath -= Update;
        }

        /// <summary>Updates the AI's destination every frame</summary>
        private void Update () {
            if (target && _ai != null) _ai.destination = target.position;
        }
    }
}
