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

    // To do: implement randomization/random reordering method. 
}
