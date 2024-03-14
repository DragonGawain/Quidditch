
using UnityEngine;


namespace Astar{ 
    public class Node : IHeapItem<Node>
    {
        public bool walkable;
        public Vector3 worldPosition;
        public int gridX, gridY, gridZ;//position in grid
        public int gCost; //distance from start node
        public int hCost;//heuristic cost (distance to target node)

        public int index;//index in the heap

        public GameObject testSphere;

        public Node parent;

        public int fCost
        {
            get { return gCost + hCost; }
        }

        public Node(bool _walkable, Vector3 pos, int gX, int gY, int gZ)
        {
            walkable = _walkable;
            worldPosition = pos;
            gridX = gX;
            gridY = gY;
            gridZ = gZ;
        }

        //Implement IHeapItem
        public int HeapIndex
        {
            get { return index; }
            set { index = value; }
        }

        public int CompareTo(Node other)
        {
            int compare = fCost.CompareTo(other.fCost);
            if (compare == 0)
            {
                //use hcost as a tie breaker
                compare = hCost.CompareTo(other.hCost);
            }
            //using heap as a min heap, need to swap the result to indicate a smaller value has a higher priority than a larger one
            return -compare;
        }
    }
}