using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace MaiNull.AStar
{
    [Serializable]
    public struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> Callback;

        public PathRequest(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
        {
            this.pathStart = pathStart;
            this.pathEnd = pathEnd;
            Callback = callback;
        }
    }
    
    [Serializable]
    public struct PathResult
    {
        public Vector3[] path;
        public bool success;
        public Action<Vector3[], bool> Callback;

        public PathResult(Vector3[] path, bool success, Action<Vector3[], bool> callback)
        {
            this.path = path;
            this.success = success;
            Callback = callback;
        }
    }
    
    [RequireComponent(typeof(Pathfinding))]
    public class PathRequestManager : MonoBehaviour
    {
        private Queue<PathResult> _results = new Queue<PathResult>();
        
        private Pathfinding _pathfinding;
        private static PathRequestManager _instance;
        
        
        private void Awake()
        {
            _instance = this;
            _pathfinding = GetComponent<Pathfinding>();
        }

        private void Update()
        {
            if (_results.Count <= 0) return;
            
            int itemsInQueue = _results.Count;
            lock (_results)
            {
                for (int i = 0; i < itemsInQueue; i++)
                {
                    PathResult result = _results.Dequeue();
                    result.Callback(result.path, result.success);
                }
            }
        }

        public static void RequestPath(PathRequest pathRequest)
        {
            ThreadStart threadStart = delegate
            {
                _instance._pathfinding.FindPath(pathRequest, _instance.FinishedProcessingPath);
            };
            threadStart.Invoke();
        }

        public void FinishedProcessingPath(PathResult result)
        {
            lock (_results)
            {
                _results.Enqueue(result);
            }
        }
    }
}