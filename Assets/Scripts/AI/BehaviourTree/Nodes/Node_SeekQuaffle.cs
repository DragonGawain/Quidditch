using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_SeekQuaffle : Node
{
    // CONSTRUCTORS
    public Node_SeekQuaffle(BehaviourTree parentTree) : base(parentTree) { }


    // METHODS
    public override NodeState Execute()
    {
        //Debug.Log("Executing SeekQuaffle");

        // Find the snitch.
        GameObject theQuaffle = ReadFromBlackboard("quaffle") as GameObject;
        if (theQuaffle == null)
        {
            theQuaffle = MyParentTree.MyGroupAI.TheQuaffle.gameObject;
            WriteToBlackboard("quaffle", theQuaffle);
        }

        // Seek it and return running.
        Vector3 desiredVelocity = MyParentTree.MyNPCMovement.KinematicSeek(theQuaffle.transform.position, 1f);
        MyParentTree.gameObject.transform.position += desiredVelocity * Time.deltaTime;
        // To do: implement proper speed, either here or within the base chaser tree.
        // To do: gooder movement behaviour. Obstacle avoidance, pathfinding, etc.

        myState = NodeState.RUNNING;
        return myState;
    }
}
