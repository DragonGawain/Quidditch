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
