using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// "->"
public class NodeSequence : NodeBranch
{
    // CONSTRUCTORS
    public NodeSequence(BehaviourTree parentTree, List<Node> childNodes = null) : base(parentTree, childNodes) { }


    // METHODS.
    override public NodeState Execute()
    {
        foreach (Node child in myChildNodes)
        {
            NodeState childsState = child.Execute();

            Debug.Log("In Sequence, checking the state of child " + child + " : " + childsState);

            switch (childsState)
            {
                case NodeState.RUNNING:
                    myState = NodeState.RUNNING;
                    Debug.Log("Should return RUNNING");
                    return myState;

                case NodeState.FAILURE:
                    Debug.Log("Should return FAILURE");
                    myState = NodeState.FAILURE;
                    return myState;

                case NodeState.SUCCESS:
                    continue;

                default:
                    continue;
            }
        }

        Debug.Log("Should return SUCCESS");

        // Should they all succeed, return true.
        myState = NodeState.SUCCESS;
        return myState;
    }
}
