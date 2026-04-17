using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MaiNull.AStar
{
    public class Grid : MonoBehaviour
    {
        public Transform player;
        [SerializeField] private LayerMask unwalkableMask;
        public Vector2 gridWorldSize;
        public float nodeRadius;
        private Node[,] _grid;
        
        private float _nodeDiameter;
        private int _gridSizeX, _gridSizeY;
        
        public List<Node> Path { get; set; }

        private void Start()
        {
            _nodeDiameter = nodeRadius * 2;
            _gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeRadius);
            _gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeRadius);
            CreateGrid();
        }

        private void CreateGrid()
        {
            _grid = new Node[_gridSizeX, _gridSizeY];
            Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 -  Vector3.forward * gridWorldSize.y / 2;
            
            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    Vector3 worldPoint =  worldBottomLeft + Vector3.right * (x * _nodeDiameter + nodeRadius) + Vector3.up * (y * _nodeDiameter + nodeRadius);
                    bool walkable = !Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask);
                    _grid[x,y] = new Node(walkable, worldPoint, x, y);
                }
            }
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;
                    
                    int checkX = node.GridX + x;
                    int checkY = node.GridY + y;

                    if (checkX >= 0 && checkX < gridWorldSize.x && checkY >= 0 && checkY < gridWorldSize.y)
                    {
                        neighbours.Add(_grid[checkX, checkY]);
                    }
                }
            }
            
            return neighbours;
        }
        
        public Node NodeFromWorldPoint(Vector3 worldPosition)
        {
            float percentX = (worldPosition.x - transform.position.x) / gridWorldSize.x;
            float percentY = (worldPosition.z - transform.position.y) / gridWorldSize.y;
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);
            
            int x = Mathf.RoundToInt((gridWorldSize.x - 1) * percentX);
            int y = Mathf.RoundToInt((gridWorldSize.y - 1) * percentY);
            return _grid[x, y];
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

            if (_grid == null) return;
            
            Node playerNode = NodeFromWorldPoint(player.position);
            
            foreach (Node node in _grid)
            {
                Gizmos.color = node.Walkable ? Color.green : Color.red;
                if (playerNode == node)
                {
                    Gizmos.color = Color.cyan;
                }

                if (Path != null)
                {
                    if (Path.Contains(node))
                    {
                        Gizmos.color = Color.yellow;
                    }
                }
                Gizmos.DrawCube(node.WorldPosition, Vector3.one * nodeRadius);
            }
        }
    }
}