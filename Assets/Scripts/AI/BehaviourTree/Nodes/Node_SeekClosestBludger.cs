using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_SeekClosestBludger : Node
{
    // CONSTRUCTORS
    public Node_SeekClosestBludger(BehaviourTree parentTree)
        : base(parentTree) { }

    // METHODS
    public override NodeState Execute()
    {
        //Debug.Log("Executing SeekClosestBludger");

        // Find the closest bludger.
        GameObject closestBludger = ReadFromBlackboard("closestBludger") as GameObject;
        closestBludger = MyParentTree.LocateClosestBludger(closestBludger).Item1;
        WriteToBlackboard("closestBludger", closestBludger);

        // To do: should we mention the target bludger?

        // Seek it and return running.
        Vector3 desiredVelocity = MyParentTree.MyNPCMovement.Seek(closestBludger.transform.position, MyParentTree.MyMaxSpeed);
        MyParentTree.SetVelocity(desiredVelocity);
        // TODO: gooder movement behaviour. Obstacle avoidance, pathfinding, etc. - should be done

        myState = NodeState.RUNNING;
        return myState;
    }
}
