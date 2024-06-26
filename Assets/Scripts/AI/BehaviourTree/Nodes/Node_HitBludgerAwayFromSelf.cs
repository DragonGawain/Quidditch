using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    // Propulse the bludger away from self.
    public class Node_HitBludgerAwayFromSelf : Node
    {
        // CONSTRUCTORS
        public Node_HitBludgerAwayFromSelf(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            // First verify that a bludger actually exists.
            GameObject theBludgerGO = ReadFromBlackboard("targetBludger") as GameObject;
            Bludger theBludger = theBludgerGO.GetComponent<Bludger>();

            if (theBludgerGO != null && theBludger != null)
            {
                // We are assuming it's already in hitting range.
                Debug.Log(string.Format("{0} hit the Bludger {1}!", MyParentTree.gameObject, theBludgerGO));

                // Determine hit force vector.
                Vector3 desiredThrow = -1 * theBludger.MyRigidbody.velocity.normalized;
                theBludger.Throw(desiredThrow.normalized * MyParentTree.BallAddedForceMultiplier, MyParentTree.gameObject);

                myState = NodeState.SUCCESS;
                return myState;
            }
            // Else, return failure.
            myState = NodeState.FAILURE;
            return myState;
        }
    }
}