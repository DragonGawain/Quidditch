using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTest_InstantAction : Node
{
    // CONSTRUCTORS
    public NodeTest_InstantAction(BehaviourTree parentTree) : base(parentTree) { }


    // METHODS
    public override NodeState Execute()
    {
        Debug.Log("The test behaviour tree is executing an instant action!");

        myState = NodeState.SUCCESS;
        return myState;
    }
}
