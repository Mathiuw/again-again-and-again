using Pathfinding;
using UnityEngine;

namespace MaiNull
{
    public class AIChaserBehaviour : MonoBehaviour
    {
        [Header("AI Chaser Settings")]
        [SerializeField] protected bool followPlayer = false;
        private Transform target;
        private AIDestinationSetter destinationSetter;

        private void Start()
        {
            Transform playerTransform = FindFirstObjectByType<Player>()?.transform;
            if (playerTransform)
            {
                target = playerTransform;
            }

            if (followPlayer)
            {
                destinationSetter = GetComponent<AIDestinationSetter>();
                if (destinationSetter)
                {
                    destinationSetter.target = playerTransform;
                }
            }
        }
    }
}
