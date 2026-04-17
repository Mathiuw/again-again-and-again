using System;
using System.Collections.Generic;
using UnityEngine;

namespace MaiNull.AStar
{
    [RequireComponent(typeof(Grid))]
    public class Pathfinding : MonoBehaviour
    {
        private Grid _grid;

        private void Awake()
        {
            _grid = GetComponent<Grid>();
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
                
            }
        }
        
    }
}