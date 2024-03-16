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
        float distanceToClosestBludger = (closestBludger != null) ? Vector3.Distance(MyParentTree.gameObject.transform.position, closestBludger.transform.position) : float.MaxValue;
        foreach (Bludger bludger in MyParentTree.MyGroupAI.TheBludgers)
        {
            // Assuming that we have only two bludgers.
            if (bludger.gameObject != closestBludger && Vector3.Distance(MyParentTree.gameObject.transform.position, bludger.gameObject.transform.position) < distanceToClosestBludger)
            {
                closestBludger = bludger.gameObject;
                distanceToClosestBludger = Vector3.Distance(MyParentTree.gameObject.transform.position, bludger.gameObject.transform.position);

                WriteToBlackboard("closestBludger", bludger.gameObject);
                break;
            }
        }

        // Do the check.
        // To do: set the distance threshold to something legit.
        if (distanceToClosestBludger < 5f)
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
