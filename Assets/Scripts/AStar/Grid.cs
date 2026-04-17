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
                    _grid[x,y] = new Node(walkable, worldPoint);
                }
            }
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
                Gizmos.DrawCube(node.WorldPosition, Vector3.one * nodeRadius);
            }
        }
    }
}