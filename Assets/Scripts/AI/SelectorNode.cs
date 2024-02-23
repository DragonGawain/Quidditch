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


    // Randomize the node's result (success or failure).
    // Threshold: a value between 0 and 1 above which we'll consider a success.
    public bool RandomResult(float threshold = 0.5f)
    {
        return Random.Range(0f, 1f) >= threshold;
    }
}
