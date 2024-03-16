using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_AmIHoldingQuaffle : Node
{
    // CONSTRUCTORS
    public Node_AmIHoldingQuaffle(BehaviourTree parentTree) : base(parentTree) { }


    // METHODS
    public override NodeState Execute()
    {
        Debug.Log("Executing AmIHoldingQuaffle?");

        // Do the check.
        if (MyParentTree.MyGroupAI.HasBall)
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
