using System.Collections.Generic;
using UnityEngine;

namespace MaiNull.AStar
{
    [RequireComponent(typeof(Grid))]
    public class Pathfinding : MonoBehaviour
    {
        public Transform seeker, target;
        
        private Grid _grid;

        private void Awake()
        {
            _grid = GetComponent<Grid>();
        }

        // private void Start()
        // {
        //     FindPath(seeker.position, target.position);
        // }

        private void Update()
        {
            FindPath(seeker.position, target.position);
        }

        private void FindPath(Vector3 startPos, Vector3 targetPos)
        {
            Node startNode = _grid.NodeFromWorldPoint(startPos);
            Node targetnode = _grid.NodeFromWorldPoint(targetPos);

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].FCost < currentNode.FCost ||  openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost)
                    {
                        currentNode = openSet[i];   
                    }
                }
                
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetnode)
                {
                    RetracePath(startNode, currentNode);
                    return; // Path has been found
                }

                foreach (Node neighbour in _grid.GetNeighbours(currentNode))
                {
                    if(!neighbour.Walkable || closedSet.Contains(neighbour)) continue;
                           
                    int newMovementCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                    {
                        neighbour.GCost = newMovementCostToNeighbour;
                        neighbour.HCost = GetDistance(neighbour, targetnode);
                        neighbour.Parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }

        private void RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Reverse();

            _grid.Path = path;
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