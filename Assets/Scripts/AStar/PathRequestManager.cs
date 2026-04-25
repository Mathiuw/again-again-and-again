using System;
using System.Collections.Generic;
using UnityEngine;

namespace MaiNull.AStar
{
    [RequireComponent(typeof(Pathfinding))]
    public class PathRequestManager : MonoBehaviour
    {
        private Queue<PathRequest> _pathRequestQueue = new Queue<PathRequest>();
        private PathRequest _currentPathRequest;
        
        private Pathfinding _pathfinding;
        private static PathRequestManager _instance;

        private bool _isProcessingPathRequest = false;
        
        private void Awake()
        {
            _instance = this;
            _pathfinding = GetComponent<Pathfinding>();
        }

        public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
        {
            PathRequest newRequest = new(pathStart, pathEnd, callback);
            _instance._pathRequestQueue.Enqueue(newRequest);
            _instance.TryProcessNext();
        }

        private void TryProcessNext()
        {
            if (_isProcessingPathRequest || _pathRequestQueue.Count <= 0) return;
            
            _currentPathRequest = _pathRequestQueue.Dequeue();
            _isProcessingPathRequest = true;
            _pathfinding.StartFindPath(_currentPathRequest.PathStart, _currentPathRequest.PathEnd);
        }

        public void FinishedProcessingPath(Vector3[] path, bool success)
        {
            _currentPathRequest.Callback(path, success);
            _isProcessingPathRequest = false;
            TryProcessNext();
        }
        
        private struct PathRequest
        {
            public Vector3 PathStart;
            public Vector3 PathEnd;
            public Action<Vector3[], bool> Callback;

            public PathRequest(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
            {
                PathStart = pathStart;
                PathEnd = pathEnd;
                Callback = callback;
            }
        }
    }
}