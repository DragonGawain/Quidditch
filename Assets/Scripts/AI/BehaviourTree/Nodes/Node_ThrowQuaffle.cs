using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_ThrowQuaffle : Node
    {
        // CONSTRUCTORS
        public Node_ThrowQuaffle(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            Debug.Log("Executing ThrowQuaffle");

            // First verify that we are holding the quaffle
            if (MyParentTree.MyGroupAI.HasBall)
            {
                // Make sure it knows we are holding it.
                GameObject theQuaffleGO = MyParentTree.MyGroupAI.TheQuaffle.gameObject;
                Quaffle theQuaffle = theQuaffleGO.GetComponent<Quaffle>();

                if (theQuaffleGO != null && theQuaffle != null && theQuaffle.MyHolder == MyParentTree.gameObject)
                {
                    // Determine the location of the enemy goalpost in order to throw the quaffle in its direction.
                    // TODO: Adjust throw intensity, fix wonky physics.
                    Vector3 desiredThrow = MyParentTree.MyGroupAI.EnemyGoalpost.gameObject.transform.position - MyParentTree.gameObject.transform.position;
                    theQuaffle.Throw(desiredThrow);

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
