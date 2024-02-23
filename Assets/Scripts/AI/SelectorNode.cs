using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// "?"
public class SelectorNode : BranchNode
{
    override public bool Execute()
    {
        foreach (Node child in myChildNodes)
        {
            if (child.Execute())
            {
                // Return successful as soon as a child does so.
                return true;
            }
        }
        // Should they all fail, return false.
        return false;
    }

    // To do: implement randomization/coin flip method.
}
