using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_HasReachedTargetBludger : Node
    {
        // CONSTRUCTORS
        public Node_HasReachedTargetBludger(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            Debug.Log("Executing HasReachedTargetBludger?");

            // Find the target bludger.
            GameObject theTargetBludger = ReadFromBlackboard("targetBludger") as GameObject;
            if (theTargetBludger == null)
            {
                // Assume we already have a target.
                myState = NodeState.FAILURE;
                return myState;
            }

            // Do the check.
            // To do: set the distance threshold to something legit.
            if (Vector3.Distance(MyParentTree.gameObject.transform.position, theTargetBludger.transform.position) <= 1f)
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

