using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace MaiNull.AStar
{
    [RequireComponent(typeof(Grid))]
    public class Pathfinding : MonoBehaviour
    {
        private Grid _grid;
        private PathRequestManager _pathRequestManager;
        
        private void Awake()
        {
            _grid = GetComponent<Grid>();
        }

        public void FindPath(PathRequest request, Action<PathResult> callback)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            Vector3[] waypoints = Array.Empty<Vector3>();
            bool pathSuccess = false;
            
            Node startNode = _grid.NodeFromWorldPoint(request.PathStart);
            Node targetNode = _grid.NodeFromWorldPoint(request.PathEnd);

            if (startNode.Walkable && targetNode.Walkable)
            {
                Heap<Node> openSet = new Heap<Node>(_grid.MaxSize);
                HashSet<Node> closedSet = new HashSet<Node>();
                openSet.Add(startNode);

                while (openSet.Count > 0)
                {
                    Node currentNode = openSet.RemoveFirst();
                    closedSet.Add(currentNode);

                    if (currentNode == targetNode)
                    {
                        sw.Stop();
                        print($"Path found: {sw.ElapsedMilliseconds} ms");
                        pathSuccess = true;
                        break; // Path has been found
                    }

                    foreach (Node neighbour in _grid.GetNeighbours(currentNode))
                    {
                        if(!neighbour.Walkable || closedSet.Contains(neighbour)) continue;
                           
                        int newMovementCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour) + neighbour.MovementPenalty;
                    
                        if (newMovementCostToNeighbour >= neighbour.GCost && openSet.Contains(neighbour)) continue;
                    
                        neighbour.GCost = newMovementCostToNeighbour;
                        neighbour.HCost = GetDistance(neighbour, targetNode);
                        neighbour.Parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                        else
                        {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
            if (pathSuccess)
            {
                waypoints = RetracePath(startNode, targetNode);
                pathSuccess = waypoints.Length > 0;
            }
            callback(new PathResult(waypoints, pathSuccess, request.Callback));
        }

        private Vector3[] RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            
            Vector3[] waypoints = SimplifyPath(path);
            Array.Reverse(waypoints);
            
            return waypoints;
        }

        private Vector3[] SimplifyPath(List<Node> path)
        {
            List<Vector3> waypoints = new List<Vector3>();
            Vector2 directionOld = Vector2.zero;

            for (int i = 1; i < path.Count; i++)
            {   
                Vector2 directionNew = new Vector2(path[i-1].GridX - path[i].GridX, path[i-1].GridY - path[i].GridY);
                if (directionNew != directionOld)
                {
                    waypoints.Add(path[i].WorldPosition);
                }
                directionOld = directionNew;
            }
            
            return waypoints.ToArray();
        }
        
        private int GetDistance(Node a, Node b)
        {
            int dstX = Mathf.Abs(a.GridX - b.GridX);
            int dstY = Mathf.Abs(a.GridY - b.GridY);

            if (dstX > dstY)
            {
                return 14*dstY + 10*(dstX - dstY);
            }
            return 14*dstX + 10*(dstY - dstX);
        }
    }
}