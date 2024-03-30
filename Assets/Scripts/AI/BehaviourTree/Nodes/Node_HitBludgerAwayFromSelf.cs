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
            Debug.Log("Executing HitBludgerAwayFromSelf");

            // First verify that we are touching a bludger.
            if (true) // TODO: incorporate bludger check.
            {
                // Make sure it knows we are touching it.
                GameObject theBludgerGO = ReadFromBlackboard("targetBludger") as GameObject;
                Bludger theBludger = theBludgerGO.GetComponent<Bludger>();

                if (theBludgerGO != null && theBludger != null && theBludger.MyHitter == MyParentTree.gameObject)
                {
                    // Determine hit force vector.
                    Vector3 desiredThrow = -1 * theBludger.MyRigidbody.velocity;
                    theBludger.Throw(desiredThrow.normalized * MyParentTree.BallAddedForceMultiplier);

                    myState = NodeState.SUCCESS;
                    return myState;
                }
            }
            // Else, return failure.
            myState = NodeState.FAILURE;
            return myState;
        }
    }
}