
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

namespace Astar
{
    using Astar;
    using Unity.VisualScripting;
    using UnityEditor;

    public class Grid : MonoBehaviour
    {
        //must be cubes (or perfect squares)
        public Transform player;
        public LayerMask unwalkableMask;

        [SerializeField]
        GameObject environmentX,environmentY,environmentZ,center; //bases the size of the grid off of another game object. If any (not X) left null, will base off of X
        [SerializeField]
        int numberOfNodes = 100; // adjust number of nodes in grid
        public Vector3 worldSize; // area in world coords that the grid covers
        public Vector3 centerWorldPosition;
        public float nodeRadius = 1; //how much space each individual node covers (from center so this is half)
        public float diagonal2D, diagonal3D;
        public Node[,,] grid;
        private int xsize,ysize,zsize;
        public GameObject[,,] cubes;

        public int maxSize
        {
            get { return xsize * ysize * zsize; }
        } 
        [SerializeField]
        public bool drawGizmos, showGridPositions;

        public List<Node> path;

        void Awake()
        {
            Assert.AreNotEqual(0, nodeRadius, "Node radius is 0");
            
            Assert.IsNotNull(player, "Player is null in Grid");
            initDimensions();


            xsize = (int)Mathf.Floor(worldSize.x / (nodeRadius * 2));
            ysize = (int)Mathf.Floor(worldSize.y / (nodeRadius * 2));
            zsize = (int)Mathf.Floor(worldSize.z / (nodeRadius * 2));

            numberOfNodes = xsize * ysize * zsize;

            CreateGrid(xsize, ysize, zsize);
            Assert.IsNotNull(player, "Grid player is null");
            diagonal2D = getDiagonal2D();
            diagonal3D = getDiagonal3D();

            //debugging
        }

        void OnDrawGizmos()
        {
            if (!drawGizmos) return;

            Color blank = new Color(1, 1, 1, 0.2f); //pale white
            Color pathFill = new Color(0, 0, 0, 0.5f); //more opaque black
            Color obstacle = Color.red;
            obstacle.a = 0.2f;
            Color highlight = Color.yellow;
            highlight.a = 0.5f;

            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(this.transform.position, new Vector3(worldSize.x, worldSize.y, worldSize.z));

            if (grid != null)
            {
                Node playernode = GetNode(player.position);
                foreach (Node n in grid)
                {
                    Handles.Label(n.worldPosition, $"({n.gridX}, {n.gridY},{n.gridZ})");
                    Gizmos.color = n.walkable ? blank : obstacle; /*Color.white: Color.red;*/
                    if (path != null)
                        if (path.Contains(n))
                            Gizmos.color = pathFill/*Color.black*/;
                    if (playernode == n)
                    {
                        Gizmos.color = highlight;
                        //Debug.Log($" player (x,y): ({n.gridX},{n.gridY})");
                    }
                    //Gizmos.color = playernode == n ? Color.yellow : Gizmos.color; 
                    //Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeRadius * 2 - 0.1f));
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (1));
                }
            }
        }

        void CreateGrid(int x, int y, int z)
        {
            //starting from bottom left back corner (- Vector3.right, -Vector3.up,  - Vector3.forward)
            //then it is just a matter of adding nodes until the grid is populated.
            grid = new Node[x, y, z];

            Vector3 worldBotLeft = transform.position - Vector3.right * worldSize.x / 2;
            worldBotLeft -= Vector3.forward * worldSize.z / 2;
            worldBotLeft -= Vector3.up * worldSize.y / 2;
            //loop through all positions of the nodes to check if they are walkable
            for (int _x = 0; _x < x; _x++)
            {
                for (int _y = 0; _y < y; _y++)
                {
                    for (int _z = 0; _z < z; _z++)
                    {
                        Vector3 worldPoint = worldBotLeft;
                        worldPoint += Vector3.right * (_x * (nodeRadius * 2) + nodeRadius);
                        worldPoint += Vector3.up * (_y * (nodeRadius * 2) + nodeRadius);
                        worldPoint += Vector3.forward * (_z * (nodeRadius * 2) + nodeRadius); //as x and y increases we go by increments 
                        bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask)); //if we dont collide with anything 
                        grid[_x, _y, _z] = new Node(walkable, worldPoint, _x, _y, _z);

                    }
                }
            }
        }
        public Node GetNode(Vector3 worldPosition)
        {//get a node from world coordinates
         //convert world position into a percentage
         // for x if on far left it will be 0, middle 0.5, far right 1

            if (worldSize.y == 0 || worldSize.x == 0 || worldSize.z == 0)
            {
                Debug.Log("World dimension = 0, cannot get node");
                return null;
            }
            Vector3 relativePosition = worldPosition - centerWorldPosition;
            
            //use postion relative to a fixed assigned center object - less messy
            //that way x and z are centered at 0 so there are - and positive vals
            //we just assume y is never negative because why would a pathfinding node be underground
            float percentX = (relativePosition.x + (worldSize.x / 2)) / worldSize.x;
            float percentY = (relativePosition.y + (worldSize.y / 2)) / worldSize.y;
            float percentZ = (relativePosition.z + (worldSize.z / 2)) / worldSize.z;
            //if character is outside of grid, we dont want invalid inputs
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);
            percentZ = Mathf.Clamp01(percentZ);
            //convert from percentage into an array index (hence -1) 
            int x = (int)Mathf.Floor((grid.GetLength(0) - 1) * percentX);
            int y = (int)Mathf.Floor((grid.GetLength(1) - 1) * percentY);
            int z = (int)Mathf.Floor((grid.GetLength(2) - 1) * percentZ);

            /*
            //assuming x and z are centered at 0 so there are - and positive vals
            float percentX = (worldPosition.x + (worldSize.x / 2)) / worldSize.x;
            float percentY = (worldPosition.y) / worldSize.y;
            float percentZ = (worldPosition.z + (worldSize.z / 2)) / worldSize.z;
            //if character is outside of grid, we dont want invalid inputs
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);
            percentZ = Mathf.Clamp01(percentZ);
    
            //int x = Mathf.RoundToInt((grid.GetLength(0)-1) * percentX);
            //int y = Mathf.RoundToInt((grid.GetLength(1) - 1) * percentY);
            //int z = Mathf.RoundToInt((grid.GetLength(2)-1) * percentZ);
            
            //convert from percentage into an array index (hence -1) 
            int x = (int)Mathf.Floor((grid.GetLength(0)-1) * percentX);
            int y = (int)Mathf.Floor((grid.GetLength(1)-1) * percentY);
            int z = (int)Mathf.Floor((grid.GetLength(2)-1) * percentZ);*/
            return grid[x, y, z];
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            //searching in a 3x3 around node
            for (int _x = -1; _x <= 1; _x++)
            {
                for (int _y = -1; _y <= 1; _y++)
                {
                    for (int _z = -1; _z <= 1; _z++)
                    {
                        if (_x == 0 && _y == 0 & _z == 0) { continue; } //y=0 x=0 z=0 is just node

                        int x = node.gridX + _x;
                        int y = node.gridY + _y;
                        int z = node.gridZ + _z;

                        if (x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1) && z >= 0 && y < grid.GetLength(2))
                        {
                            neighbours.Add(grid[x, y, z]);
                        }
                    }
                }
            }

            return neighbours;
        }
        float getDiagonal2D()
        {//calc diagonal distance of a square (touching on the sides)
            return (float)((int)Mathf.Sqrt(nodeRadius * 2) * 10);
        }
        float getDiagonal3D()
        {//calc diagonal distance 3D (on a corner not touching any sides)
            return (float)((int)Mathf.Sqrt(nodeRadius * 2) * 10);
        }

        public float getDistance(Vector3 start, Vector3 end)
        {//euclidean
            float expression = (end.x - start.x) * (end.x - start.x) + (end.y - start.y) * (end.y - start.y) + (end.z - start.z) * (end.z - start.z);
            return Mathf.Sqrt(expression);
        }

        private void initDimensions()
        {

            Assert.IsNotNull(environmentX);
            Vector3 gridPos = Vector3.one; 
            if (environmentY == null)
            {
                environmentY = environmentX;
            }
            if (environmentZ == null)
            {
                environmentZ = environmentX;
            }
            if(center == null)
            {
                center = environmentX;
            }
            //Vector3 envPos = new Vector3(environmentX.transform.position.x, environmentY.transform.position.y, environmentZ.transform.position.z);
            

            worldSize = Vector3.one;
            Renderer r = environmentX.GetComponentInChildren<Renderer>();
            worldSize.x = r.bounds.size.x != 0 ? r.bounds.size.x : 1;
            r = environmentY.GetComponentInChildren<Renderer>();
            worldSize.y = r.bounds.size.y != 0 ? r.bounds.size.y : 1;
            r = environmentZ.GetComponentInChildren<Renderer>();
            worldSize.z = r.bounds.size.z != 0 ? r.bounds.size.z : 1;

            centerWorldPosition = center.transform.position;
            centerWorldPosition.y = worldSize.y/2;//we just assume y is never in the negative direction because why would a pathfinding node be underground
            
            transform.position = centerWorldPosition;


                //if (environmentX != null)
                //{
                //    Renderer r = environment.GetComponentInChildren<Renderer>();
                //    Assert.IsNotNull(r, "Environment used for size does not have renderer attached, Attempting to use Collider");
                //    if(r == null)
                //    {
                //        Collider c = environmentX.GetComponentInChildren<Collider>();
                //        worldSize = c.bounds.size;
                //        Assert.IsNotNull(c, "Environment used for size does not have Collider attached");
                //    }
                //    else
                //        worldSize = r.bounds.size;

                //}else
                //   worldSize = Vector3.one * 100;

                //Assert.AreEqual(Vector3.zero, worldSize, "World size is 0 Vector");

                //how many nodes based on node radius do we need for the grid
            }
    }
}
