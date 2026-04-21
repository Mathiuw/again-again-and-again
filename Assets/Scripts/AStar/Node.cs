using UnityEngine;

namespace MaiNull.AStar
{
    public class Node
    {
        public bool Walkable;
        public Vector3 WorldPosition;
        public int GridX;
        public int GridY;

        public int GCost;
        public int HCost;
        public Node Parent;
        
        public int FCost => GCost + HCost;
        
        public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
        {
            Walkable = walkable;
            WorldPosition = worldPosition;
            GridX = gridX;
            GridY = gridY;
        }
    }
}