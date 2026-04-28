using System;
using System.Collections.Generic;
using UnityEngine;

namespace MaiNull.AStar
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private LayerMask unwalkableMask;
        public Vector2 gridWorldSize;
        public float nodeRadius;
        [SerializeField] private bool displayGridGizmos;
        private Node[,] _grid;

        [SerializeField] private TerrainType[] walkableRegions;
        public int obstacleProximityPenalty = 10;
        [SerializeField] private LayerMask walkableMask;
        private Dictionary<int, int> _walkableRegionsDict = new Dictionary<int, int>();

        private float NodeDiameter => nodeRadius * 2;
        private int _gridSizeX, _gridSizeY;

        private int _penaltyMin = int.MaxValue;
        private int _penaltyMax = int.MinValue;
        
        public int MaxSize => _gridSizeX * _gridSizeY;

        private void Awake()
        {
            _gridSizeX = Mathf.RoundToInt(gridWorldSize.x / NodeDiameter);
            _gridSizeY = Mathf.RoundToInt(gridWorldSize.y / NodeDiameter);

            foreach (TerrainType region in walkableRegions)
            {
                walkableMask.value += region.TerrainMask.value;
                _walkableRegionsDict.Add( (int)Mathf.Log(region.TerrainMask.value, 2), region.TerrainPenalty);
            }
            
            CreateGrid();
        }

        private void CreateGrid()
        {
            _grid = new Node[_gridSizeX, _gridSizeY];
            Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 -  Vector3.forward * gridWorldSize.y / 2;
            // Debug.Log(worldBottomLeft);
            
            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    Vector3 worldPoint =  worldBottomLeft + Vector3.right * (x * NodeDiameter + nodeRadius) + Vector3.forward * (y * NodeDiameter + nodeRadius);
                    bool walkable = !Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask);
                    
                    int movementPenalty = 0;

                    Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down);
                    if (Physics.Raycast(ray, out RaycastHit hit, 100, walkableMask))
                    {
                        _walkableRegionsDict.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);
                    }

                    if (!walkable)
                    {
                        movementPenalty += obstacleProximityPenalty;
                    }
                    
                    _grid[x,y] = new Node(walkable, worldPoint, x, y, movementPenalty);
                }
            }
            
            BlurPenaltyMap(3);
        }

        private void BlurPenaltyMap(int blueSize)
        {
            int kernelSize = blueSize * 2 + 1;
            int kernelExtents = (kernelSize - 1) / 2;
            
            int[,] penaltiesHorizontalPass =  new int[_gridSizeX, _gridSizeY];
            int[,] penaltiesVerticalPass =  new int[_gridSizeX, _gridSizeY];

            for (int y = 0; y < _gridSizeY; y++)
            {
                for (int x = -kernelExtents; x < kernelExtents; x++)
                {
                    int sampleX = Mathf.Clamp(x, 0, kernelExtents);
                    penaltiesHorizontalPass[0, y] += _grid[sampleX, y].MovementPenalty;
                }
                
                for (int x = 1; x < _gridSizeX; x++)
                {
                    int removeIndex = Mathf.Clamp(x - kernelExtents - 1, 0, _gridSizeX);
                    int addIndex = Mathf.Clamp(x + kernelExtents, 0, _gridSizeX - 1); 
                    
                    penaltiesHorizontalPass[x,y] = penaltiesHorizontalPass[x-1, y] - _grid[removeIndex, y].MovementPenalty + _grid[addIndex, y].MovementPenalty;
                }
            }
            
            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = -kernelExtents; y < kernelExtents; y++)
                {
                    int sampleY = Mathf.Clamp(y, 0, kernelExtents);
                    penaltiesVerticalPass[x, 0] += penaltiesHorizontalPass[x, sampleY];
                }
                
                int blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, 0] / (kernelSize * kernelSize));
                _grid[x, 0].MovementPenalty = blurredPenalty;
                
                for (int y = 1; y < _gridSizeY; y++)
                {
                    int removeIndex = Mathf.Clamp(y - kernelExtents - 1, 0, _gridSizeY);
                    int addIndex = Mathf.Clamp(y + kernelExtents, 0, _gridSizeY - 1); 
                    
                    penaltiesVerticalPass[x,y] = penaltiesVerticalPass[x, y-1] - penaltiesHorizontalPass[x, removeIndex] + penaltiesHorizontalPass [x, addIndex];
                    blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, y] / (kernelSize * kernelSize));
                    _grid[x, y].MovementPenalty = blurredPenalty;

                    if (blurredPenalty > _penaltyMax) _penaltyMax = blurredPenalty;
                    if (blurredPenalty < _penaltyMin) _penaltyMin = blurredPenalty;
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
            float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
            float percentY = (worldPosition.z + gridWorldSize.y/2) / gridWorldSize.y;
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
            
            if (_grid == null || !displayGridGizmos) return;
            
            foreach (Node node in _grid)
            {
                Gizmos.color = Color.Lerp(Color.white, Color.black,
                    Mathf.InverseLerp(_penaltyMin, _penaltyMax, node.MovementPenalty));
                
                Gizmos.color = node.Walkable ? Gizmos.color : Color.red;
                Gizmos.DrawCube(node.WorldPosition, Vector3.one * (NodeDiameter - .1f));
            }
        }
    }
    
    [Serializable]
    public class TerrainType
    {
        public LayerMask TerrainMask;
        public int TerrainPenalty;
    }
    
}