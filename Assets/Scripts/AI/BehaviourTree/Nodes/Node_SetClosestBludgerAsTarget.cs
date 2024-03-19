using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_SetClosestBludgerAsTarget : Node
{
    // CONSTRUCTORS
    public Node_SetClosestBludgerAsTarget(BehaviourTree parentTree) : base(parentTree) { }


    // METHODS
    public override NodeState Execute()
    {
        //Debug.Log("Executing SetClosestBludgerAsTarget");

        // Find the closest bludger.
        GameObject closestBludger = ReadFromBlackboard("closestBludger") as GameObject;
        closestBludger = MyParentTree.LocateClosestBludger(closestBludger).Item1;
        WriteToBlackboard("closestBludger", closestBludger);

        // Set and return.
        if (closestBludger)
        {
            WriteToBlackboard("targetBludger", closestBludger);
            myState = NodeState.SUCCESS;
            return myState;
        }
        else
        {
            myState = NodeState.FAILURE;
            return myState;
        }
    }
}
