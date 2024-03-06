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
                    anyChildIsRunning = true;
                    continue;

                default:
                    continue;
            }
        }
        // Should they all succeed, return true.
        myState = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return myState;
    }


    // Randomize the order of child nodes.
    public void ShuffleChildren()
    {
        // Reference: https://stackoverflow.com/questions/273313/randomize-a-listt

        int noOfChildNodes = myChildNodes.Count;
        while (noOfChildNodes > 1)
        {
            noOfChildNodes--;

            int newId = Random.Range(0, noOfChildNodes + 1);
            Node currentNode = myChildNodes[newId];
            myChildNodes[newId] = myChildNodes[noOfChildNodes];
            myChildNodes[noOfChildNodes] = currentNode;
        }
    }
}
