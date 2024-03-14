using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_SeekSnitch : Node
{
    // CONSTRUCTORS
    public Node_SeekSnitch(BehaviourTree parentTree) : base(parentTree) { }


    // METHODS
    public override NodeState Execute()
    {
        //Debug.Log("Executing SeekSnitch");

        // Find the snitch.
        GameObject theSnitch = ReadFromBlackboard("snitch") as GameObject;
        if (theSnitch == null)
        {
            theSnitch = GameObject.FindGameObjectWithTag("snitch");
        }

        // Seek it and return running.
        Vector3 desiredVelocity = MyParentTree.MyNPCMovement.KinematicSeek(theSnitch.transform.position, 1f);
        MyParentTree.gameObject.transform.position += desiredVelocity * Time.deltaTime;
        // To do: gooder movement behaviour. Obstacle avoidance, pathfinding, etc.

        myState = NodeState.RUNNING;
        return myState;
    }
}