using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_IsClosestBludgerTooClose : Node
{
    // CONSTRUCTORS
    public Node_IsClosestBludgerTooClose(BehaviourTree parentTree) : base(parentTree) { }


    // METHODS
    public override NodeState Execute()
    {
        //Debug.Log("Executing IsClosestBludgerTooClose");

        // Find the closest bludger.
        GameObject closestBludger = ReadFromBlackboard("closestBludger") as GameObject;
        (GameObject, float) closestBludgerAndDistance = MyParentTree.LocateClosestBludger(closestBludger);
        WriteToBlackboard("closestBludger", closestBludgerAndDistance.Item1);

        // Do the check.
        if (closestBludgerAndDistance.Item2 < 5f) // To do: set the distance threshold to something legit.
        {
            myState = NodeState.SUCCESS;
        }
        else
        {
            myState = NodeState.FAILURE;
        }
        return myState;
    }
}
