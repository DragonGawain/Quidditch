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
        closestBludger = MyParentTree.LocateClosestBludger(closestBludger).Item1;
        WriteToBlackboard("closestBludger", closestBludger);

        // To do: should we mention the target bludger?

        // Seek it and return running.
        Vector3 desiredVelocity = MyParentTree.MyNPCMovement.KinematicSeek(closestBludger.transform.position, MyParentTree.MyMaxSpeed);
        MyParentTree.gameObject.transform.position += desiredVelocity * Time.deltaTime;
        // To do: gooder movement behaviour. Obstacle avoidance, pathfinding, etc.

        myState = NodeState.RUNNING;
        return myState;
    }
}
