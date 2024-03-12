using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// "?"
public class SelectorNode : BranchNode
{
    // CONSTRUCTORS
    public SelectorNode(List<Node> childNodes = null, Node parentNode = null) : base(childNodes, parentNode) { }


    // METHODS.
    override public NodeState Execute()
    {
        bool anyChildIsRunning = false;

        foreach (Node child in myChildNodes)
        {
            switch(child.Execute())
            {
                case NodeState.FAILURE:
                    continue;

                case NodeState.SUCCESS:
                    myState = NodeState.SUCCESS;
                    return myState;

                case NodeState.RUNNING:
                    anyChildIsRunning = true; // Could we simply return here?

                    myState = NodeState.RUNNING;
                    return myState;

                    //continue;

                default:
                    continue;
            }
        }
        // Should they all fail, return false.
        myState = anyChildIsRunning ? NodeState.RUNNING : NodeState.FAILURE;
        return myState;
    }
}
