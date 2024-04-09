using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    // Propulse the bludger towards the given target.
    public class Node_HitBludgerTowardsTarget : Node
    {
        // CONSTRUCTORS
        public Node_HitBludgerTowardsTarget(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            Debug.Log("Executing HitBludgerTowardsTarget");

            // First verify that we are touching a bludger.
            if (true) // TODO: incorporate bludger check.
            {
                // Make sure it knows we are holding it.
                GameObject theBludgerGO = ReadFromBlackboard("targetBludger") as GameObject;
                Bludger theBludger = theBludgerGO.GetComponent<Bludger>();

                // Fetch protected target.
                GameObject enemyTarget = ReadFromBlackboard("attackTarget") as GameObject;

                if (enemyTarget != null && theBludgerGO != null && theBludger != null) // && theBludger.MyHitter == MyParentTree.gameObject)
                {
                    // Determine hit force vector.
                    Vector3 desiredThrow = enemyTarget.transform.position - MyParentTree.gameObject.transform.position;
                    theBludger.Throw(desiredThrow.normalized * MyParentTree.BallAddedForceMultiplier, MyParentTree.gameObject, enemyTarget);

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