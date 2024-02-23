using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// "->"
public class SequenceNode : BranchNode
{
    override public bool Execute()
    {
        foreach (Node child in myChildNodes)
        {
            if (!child.Execute())
            {
                // Return failed as soon as a child does so.
                return false;
            }
        }
        // Should they all succeed, return true.
        return true;
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
