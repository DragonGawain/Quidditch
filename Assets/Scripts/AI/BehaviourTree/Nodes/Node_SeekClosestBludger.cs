using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_SeekClosestBludger : Node
{
    // CONSTRUCTORS
    public Node_SeekClosestBludger(BehaviourTree parentTree) : base(parentTree) { }



    // METHODS
    public override NodeState Execute()
    {
        //Debug.Log("Executing SeekClosestBludger");

        // Find the closest bludger.
        GameObject closestBludger = ReadFromBlackboard("closestBludger") as GameObject;
        float distanceToClosestBludger = (closestBludger != null) ? Vector3.Distance(MyParentTree.gameObject.transform.position, closestBludger.transform.position) : float.MaxValue;
        foreach (Bludger bludger in MyParentTree.MyGroupAI.TheBludgers)
        {
            // Assuming that we have only two bludgers.
            if (bludger.gameObject != closestBludger && Vector3.Distance(MyParentTree.gameObject.transform.position, bludger.gameObject.transform.position) < distanceToClosestBludger)
            {
                closestBludger = bludger.gameObject;
                distanceToClosestBludger = Vector3.Distance(MyParentTree.gameObject.transform.position, bludger.gameObject.transform.position);

                WriteToBlackboard("closestBludger", bludger.gameObject);
                break;
            }
        }

        // Seek it and return running.
        Vector3 desiredVelocity = MyParentTree.MyNPCMovement.KinematicSeek(closestBludger.transform.position, 1f);
        MyParentTree.gameObject.transform.position += desiredVelocity * Time.deltaTime;
        // To do: implement proper speed, either here or within the base beater tree.
        // To do: gooder movement behaviour. Obstacle avoidance, pathfinding, etc.

        myState = NodeState.RUNNING;
        return myState;
    }
}
