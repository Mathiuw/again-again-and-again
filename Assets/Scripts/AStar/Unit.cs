using System;
using System.Collections;
using UnityEngine;

namespace MaiNull.AStar
{
    public class Unit : MonoBehaviour
    {
        private const float MinPathUpdateTime = 0.2f;
        private const float PathUpdateMoveThreshold = 0.5f;
        
        [SerializeField] private Transform target;
        [SerializeField] private float moveSpeed = 15f;
        [SerializeField] private float turnSpeed = 3;
        [SerializeField] private float turnDist = 5f;
        public float stoppingDist = 10;
        private Path _path;
        
        
        public Transform Target => target;

        public float MoveSpeed => moveSpeed;

        private void Start()
        {
            StartCoroutine(UpdatePath());
        }

        private void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
        {
            if (!pathSuccessful) return;
            _path = new Path(waypoints, transform.position, turnDist, stoppingDist);
            
            StopCoroutine(FollowPath());
            StartCoroutine(FollowPath());
        }

        private IEnumerator UpdatePath()
        {
            if (Time.timeSinceLevelLoad < .3f)
            {
                yield return new WaitForSeconds(.3f);
            }
            PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
            
            float sqrMoveThreshold = PathUpdateMoveThreshold * PathUpdateMoveThreshold;
            Vector3 targetPosOld = target.position;
            
            while (true)
            {
                yield return new WaitForSeconds(MinPathUpdateTime);
                if (!((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)) continue;
                
                PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }
        
        private IEnumerator FollowPath()
        {
            bool followingPath = true;
            int pathIndex = 0;
            transform.LookAt(_path.LookPoints[0]);

            float speedPercent = 1;
            
            while (followingPath)
            {
                Vector2 position2D = new Vector2(transform.position.x, transform.position.z);
                while (_path.TurnBoundaries[pathIndex].HasCrossedLine(position2D))
                {
                    if (pathIndex == _path.FinishLineIndex)
                    {
                        followingPath = false;
                        break;
                    }
                    else pathIndex++;
                }
                
                if (followingPath)
                {
                    if (pathIndex >= _path.slowDownIndex && stoppingDist > 0)
                    {
                        speedPercent = Mathf.Clamp01(_path.TurnBoundaries[_path.FinishLineIndex].DistanceFromPoint(position2D) / stoppingDist);
                        if (speedPercent < 0.01f)
                        {
                            followingPath = false;
                        }
                    }
                    
                    Quaternion targetRotation = Quaternion.LookRotation(_path.LookPoints[pathIndex] - transform.position);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                    transform.Translate(Vector3.forward * (Time.deltaTime * moveSpeed * speedPercent), Space.Self) ;
                }
                
                yield return null;
            }
        }

        private void OnDrawGizmos()
        {
            _path?.DrawWithGizmos();
        }
    }
}