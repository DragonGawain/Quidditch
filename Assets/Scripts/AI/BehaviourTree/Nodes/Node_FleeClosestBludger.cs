using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_FleeClosestBludger : Node
{
    // CONSTRUCTORS
    public Node_FleeClosestBludger(BehaviourTree parentTree) : base(parentTree) { }


    // METHODS
    public override NodeState Execute()
    {
        //Debug.Log("Executing FleeBludger");

        // Find the closest bludger.
        GameObject closestBludger = ReadFromBlackboard("closestBludger") as GameObject;

        // Should this be done in the bludger distance measurement node instead?
        object distanceToClosestBludgerObj = ReadFromBlackboard("distanceToClosestBludger");
        float distanceToClosestBludger = (distanceToClosestBludgerObj != null) ? (float)distanceToClosestBludgerObj : float.MaxValue;

        foreach (Bludger bludger in MyParentTree.MyGroupAI.TheBludgers)
        {
            if (bludger.gameObject != closestBludger && Vector3.Distance(MyParentTree.gameObject.transform.position, bludger.gameObject.transform.position) < distanceToClosestBludger)
            {
                closestBludger = bludger.gameObject;
                WriteToBlackboard("closestBludger", bludger.gameObject);
                WriteToBlackboard("distanceToClosestBludger", Vector3.Distance(MyParentTree.gameObject.transform.position, bludger.gameObject.transform.position));
                break;
            }
        }


        // Flee it and return running.
        Vector3 desiredVelocity = MyParentTree.MyNPCMovement.KinematicFlee(closestBludger.transform.position, 1f);
        MyParentTree.gameObject.transform.position += desiredVelocity * Time.deltaTime;
        // To do: implement proper speed, either here or within the base player tree.
        // To do: gooder movement behaviour. Obstacle avoidance, pathfinding, etc.

        myState = NodeState.RUNNING;
        return myState;
    }
}
