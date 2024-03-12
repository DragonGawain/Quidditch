using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// "->"
public class SequenceNode : BranchNode
{
    // CONSTRUCTORS
    public SequenceNode(List<Node> childNodes = null, Node parentNode = null) : base(childNodes, parentNode) { }


    // METHODS.
    override public NodeState Execute()
    {
        bool anyChildIsRunning = false;

        foreach (Node child in myChildNodes)
        {
            switch (child.Execute())
            {
                case NodeState.FAILURE:
                    myState = NodeState.FAILURE;
                    return myState;

                case NodeState.SUCCESS:
                    continue;

                case NodeState.RUNNING:
                    anyChildIsRunning = true;// Could we simply return here?

                    myState = NodeState.RUNNING;
                    return myState;
                    
                    //continue;

                default:
                    continue;
            }
        }
        // Should they all succeed, return true.
        myState = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return myState;
    }
}
