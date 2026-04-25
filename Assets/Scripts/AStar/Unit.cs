using System;
using System.Collections;
using UnityEngine;

namespace MaiNull.AStar
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float moveSpeed = 15f;
        private Vector3[] _path;
        private int _targetIndex;
        
        public Transform Target => target;

        private void Start()
        {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        }

        private void OnPathFound(Vector3[] newPath, bool pathSuccessful)
        {
            if (!pathSuccessful) return;
            _path = newPath;
            
            StopCoroutine(FollowPath());
            StartCoroutine(FollowPath());
        }

        private IEnumerator FollowPath()
        {
            Vector3 currentWaypoint = _path[0];

            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    _targetIndex++;

                    if (_targetIndex >= _path.Length)
                    {
                        yield break;
                    }
                    
                    currentWaypoint = _path[_targetIndex];
                }
                
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        private void OnDrawGizmos()
        {
            if (_path == null) return;
            
            for (int i = _targetIndex; i < _path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawSphere(_path[i], 0.25f);

                if (i == _targetIndex)
                {
                    Gizmos.DrawLine(transform.position, _path[i]);
                }
                else
                {
                    Gizmos.DrawLine(_path[i-1], _path[i]);
                }
            }
        }
    }
}