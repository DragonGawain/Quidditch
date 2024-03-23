using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    // Propulse the quaffle toward the given target.
    public class Node_ThrowQuaffleAtGoalpost : Node
    {
        // CONSTRUCTORS
        public Node_ThrowQuaffleAtGoalpost(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            Debug.Log("Executing ThrowQuaffleAtTargetGoalpost");

            // TODO: use this same node for passes also?

            // First verify that we are holding the quaffle
            if (MyParentTree.MyGroupAI.HasBall)
            {
                // Make sure it knows we are holding it.
                GameObject theQuaffleGO = MyParentTree.MyGroupAI.TheQuaffle.gameObject;
                Quaffle theQuaffle = theQuaffleGO.GetComponent<Quaffle>();

                // Fetch the target goalpost.
                GameObject targetGoalpost = ReadFromBlackboard("enemyGoalpost") as GameObject;

                if (targetGoalpost != null && theQuaffleGO != null && theQuaffle != null && theQuaffle.MyHolder == MyParentTree.gameObject)
                {
                    // Determine hit force vector.
                    Vector3 desiredThrow = targetGoalpost.gameObject.transform.position - MyParentTree.gameObject.transform.position;
                    theQuaffle.Throw(desiredThrow * MyParentTree.BallAddedForceMultiplier);

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
