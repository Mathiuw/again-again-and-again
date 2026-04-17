using UnityEngine;

namespace MaiNull.AStar
{
    public class Node
    {
        public bool Walkable;
        public Vector3 WorldPosition;

        public int GCost;
        public int HCost;
        
        public int FCost => GCost + HCost;
        
        public Node(bool walkable, Vector3 worldPosition)
        {
            Walkable = walkable;
            WorldPosition = worldPosition;
        }
    }
}