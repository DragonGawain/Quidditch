using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_IsTargetBludgerWithinHitDistance : Node
    {
        // CONSTRUCTORS
        public Node_IsTargetBludgerWithinHitDistance(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing Node_IsClosestBludgerWithinHitDistance");

            // Fetch the target bludger.
            GameObject targetBludger = ReadFromBlackboard("targetBludger") as GameObject;

            // Do the check.
            if (targetBludger != null && Vector3.Distance(MyParentTree.gameObject.transform.position, targetBludger.transform.position) < MyParentTree.BludgerHitDistance) 
                // This distance threshold is set in the individual AI's root BehaviourTree script.
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
}