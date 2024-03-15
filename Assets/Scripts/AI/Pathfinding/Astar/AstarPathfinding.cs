using System.Collections;using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;


namespace Astar
{
    using Astar;
    using Unity.VisualScripting;

    public class Pathfinding : MonoBehaviour
{
    Astar.Grid grid;
    //noot optimal, since finding lowest cost is expensive, bestt to use a heap
    [SerializeField]
    Material testOpen, testPath, testClosed;

    [SerializeField]
    public bool drawNodes, drawPathOnly;

    public Transform seeker;
    public Transform target;
    //public List<Node> open = new List<Node>();
    public Heap<Node> open;
    HashSet<Node> closed = new HashSet<Node>();
    public List<Node> path;

    private static System.Timers.Timer aTimer;


    void Awake()
    {
        grid = GetComponent<Grid>();
        open = new Heap<Node>(grid.maxSize);
    }
  

    void Update()
    {
        Assert.IsNotNull(seeker, "Seeker is null");
        Assert.IsNotNull(target, "Target is null");

        FindPath(seeker.position, target.position);
    }

    void testAddToOpen(Astar.Node n)
    {
        open.Add(n);
        if ( !drawNodes || drawPathOnly) { n.disableTestSphere(); return; }

        n.enableTestSphere();
        n.testSphere.GetComponent<Renderer>().material = testOpen;

    }

    void testAddToClosed(Astar.Node n)
    {
        //just drawing a phere to visualise path quickly
        //open.Remove(n);
        closed.Add(n);

        if (!drawNodes || drawPathOnly ) { return; }
        n.testSphere.GetComponent<Renderer>().material = testClosed;

    }
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        //convert world position into Nodes
        Node start = grid.GetNode(startPos);
        Node target = grid.GetNode(targetPos);
        Node current = null;
        if (start == null) { UnityEngine.Debug.Log("Start Node is Null"); return; }
        if (target == null) { UnityEngine.Debug.Log("Target Node is Null"); return; }

            //List<Node> open = new List<Node>();
            // HashSet<Node> closed = new HashSet<Node>();
            //open.Clear();
        open.Clear();
        closed.Clear();
        //add start node to openset
        //open.Add(start);
        testAddToOpen(start);
        while (open.Count > 0)
        {//while there are still nodes that havent been explored yet
             current = open.pop();
            /*current = open[0];

            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].fCost < current.fCost || open[i].fCost == current.fCost && open[i].hCost < current.hCost)
                {
                    current = open[i];
                }
            }
            //remove current node(node with lowest f cost) from openset and add to closed set
            //open.Remove(current);
            //closed.Add(current);*/
            testAddToClosed(current);
            
            if (current.gridX == target.gridX && current.gridY == target.gridY && current.gridZ == target.gridZ)
            {//target reached, return path

                retracePath(start, target);
                return;
            }
            //loop through neighbours
            foreach (Node neighbour in grid.GetNeighbours(current))
            {
                //Debug.Log($"\t neighbour(x,y): ({neighbour.gridX},{neighbour.gridY})");
                //not walkable or on closed list, skip
                if (!neighbour.walkable || closed.Contains(neighbour)) { continue; }

                int newMovementCost = current.gCost + GetDistance(current, neighbour);
                if (newMovementCost < neighbour.gCost || !open.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCost;
                    neighbour.hCost = GetDistance(neighbour, target);
                    Assert.IsNotNull(current, "Adding null parent in Find Path");
                    neighbour.parent = current;

                    if (!open.Contains(neighbour))
                    {
                        //open.Add(neighbour);
                        testAddToOpen(neighbour);
                    }
                }
            }
        }
    }

    void retracePath(Node start, Node end)
    {
        Assert.IsNotNull(start, "Start Node is null, cant retrace path");
        Assert.IsNotNull(end, "End Node is null, cant retrace path");
        List<Node> p = new List<Node>();
        Node current = end;

        while (current != start)
        {
            if (drawNodes)
            {
                current.enableTestSphere();
                current.testSphere.GetComponent<Renderer>().material = testPath;
            }
            p.Add(current);
            current = current.parent;
            Assert.IsNotNull(current, "Current parent is null, cant retrace path");
        }
        p.Reverse();//its backwareds need to reverse it

        //visualise
        path = p;
        grid.path = p;
    }
    //get distance between 2 nodes
    int GetDistance(Node a, Node b)
    {
        //Calculate heuristic
        //in this case using euclidean


        return (int)grid.getDistance(a.worldPosition, b.worldPosition);

    }
}
}
