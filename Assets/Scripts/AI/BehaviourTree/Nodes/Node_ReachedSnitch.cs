using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_ReachedSnitch : Node
{
    // CONSTRUCTORS
    public Node_ReachedSnitch(BehaviourTree parentTree) : base(parentTree) { }


    // METHODS
    public override NodeState Execute()
    {
        Debug.Log("Executing ReachedSnitch?");

        // Find the snitch.
        GameObject theSnitch = ReadFromBlackboard("snitch") as GameObject;
        if (theSnitch == null)
        {
            theSnitch = GameObject.FindGameObjectWithTag("snitch");
        }

        if (Vector3.Distance(MyParentTree.gameObject.transform.position, theSnitch.transform.position) <= 1f)
        {
            myState = NodeState.SUCCESS;
        }
        else
        {
            myState = NodeState.FAILURE;
        }
        return myState;
    }
}
