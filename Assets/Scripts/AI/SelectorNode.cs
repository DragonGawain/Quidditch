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
                    anyChildIsRunning = true;
                    continue;

                default:
                    continue;
            }
        }
        // Should they all fail, return false.
        myState = anyChildIsRunning ? NodeState.RUNNING : NodeState.FAILURE;
        return myState;
    }


    // Randomize the node's result (success or failure).
    // Threshold: a value between 0 and 1 above which we'll consider a success.
    public NodeState RandomResult(float threshold = 0.5f)
    {
        return (Random.Range(0f, 1f) >= threshold ? NodeState.SUCCESS : NodeState.FAILURE);
    }
    // Should this be in the base / individual node instead?
}
