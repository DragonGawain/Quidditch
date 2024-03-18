using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_SeekTargetBludger : Node
{
    // CONSTRUCTORS
    public Node_SeekTargetBludger(BehaviourTree parentTree)
        : base(parentTree) { }

    // METHODS
    public override NodeState Execute()
    {
        //Debug.Log("Executing Node_SeekTargetBludger");

        // Find the target bludger.
        GameObject targetBludger = ReadFromBlackboard("targetBludger") as GameObject;
        if (targetBludger == null)
        {
            // We assume it has already been set
            myState = NodeState.FAILURE;
            return myState;
        }

        // Seek it and return running.
        Vector3 desiredVelocity = MyParentTree.MyNPCMovement.Seek(targetBludger.transform.position, MyParentTree.MyMaxSpeed);
        MyParentTree.SetVelocity(desiredVelocity);
        // To do: gooder movement behaviour. Obstacle avoidance, pathfinding, etc. - should be done

        myState = NodeState.RUNNING;
        return myState;
    }
}
