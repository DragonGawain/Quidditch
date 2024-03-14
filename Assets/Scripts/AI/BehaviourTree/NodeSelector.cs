using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// "?"
public class NodeSelector : NodeBranch
{
    // CONSTRUCTORS
    public NodeSelector(BehaviourTree parentTree, List<Node> childNodes = null) : base(parentTree, childNodes) { }


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
}
