using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_FleeClosestBludger : Node
{
    // CONSTRUCTORS
    public Node_FleeClosestBludger(BehaviourTree parentTree)
        : base(parentTree) { }

    // METHODS
    public override NodeState Execute()
    {
        //Debug.Log("Executing FleeBludger");

        // Find the closest bludger.
        GameObject closestBludger = ReadFromBlackboard("closestBludger") as GameObject;
        closestBludger = MyParentTree.LocateClosestBludger(closestBludger).Item1;
        WriteToBlackboard("closestBludger", closestBludger);

        // Flee it and return running.
        Vector3 desiredVelocity = MyParentTree.MyNPCMovement.Flee(closestBludger.transform.position, MyParentTree.MyMaxSpeed);
        MyParentTree.SetVelocity(desiredVelocity);
        // TODO: gooder movement behaviour. Obstacle avoidance, pathfinding, etc. - should be done

        myState = NodeState.RUNNING;
        return myState;
    }
}
