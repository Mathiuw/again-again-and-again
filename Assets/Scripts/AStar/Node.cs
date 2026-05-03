using UnityEngine;

namespace MaiNull.AStar
{
    public class Node : IHeapItem<Node>
    {
        public bool Walkable;
        public Vector3 WorldPosition;
        public int GridX;
        public int GridY;
        public int MovementPenalty;

        public int GCost;
        public int HCost;
        public Node Parent;
        
        public int FCost => GCost + HCost;
        
        public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY, int movementPenalty)
        {
            Walkable = walkable;
            WorldPosition = worldPosition;
            GridX = gridX;
            GridY = gridY;
            MovementPenalty = movementPenalty;
        }

        public int CompareTo(Node other)
        {
            int compare = FCost.CompareTo(other.FCost);
            if (compare == 0)
            {
                compare = HCost.CompareTo(other.HCost);
            }
            return -compare;
        }

        public int HeapIndex { get; set; }
    }
}