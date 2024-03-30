using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    // Are we near a generic target (registered by that name on the blackboard)?
    public class Node_IsNearTarget : Node
    {
        // CONSTRUCTORS
        public Node_IsNearTarget(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            Debug.Log("Executing IsNearTarget?");

            // Find the quaffle.
            GameObject theTarget = ReadFromBlackboard("target") as GameObject;
            if (theTarget == null)
            {
                myState = NodeState.FAILURE;
                return myState;
            }

            // Do the check.
            // The distance threshold is set in the individual AI's root BehaviourTree script.
            if (Vector3.Distance(MyParentTree.gameObject.transform.position, theTarget.transform.position) <= MyParentTree.FollowHasReachedDistance)
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