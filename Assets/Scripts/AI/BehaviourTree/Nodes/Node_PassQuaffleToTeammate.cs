using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CharacterAI
{
    // Propulse the quaffle toward the given target.
    public class Node_PassQuaffleToTeammate : Node
    {
        // CONSTRUCTORS
        public Node_PassQuaffleToTeammate(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing PassQuaffleToTeammate");

            // First verify that we are holding the quaffle
            if (MyParentTree.MyGroupAI.HasBall)
            {
                GameObject theQuaffleGO = MyParentTree.MyGroupAI.TheQuaffle.gameObject;
                Quaffle theQuaffle = theQuaffleGO.GetComponent<Quaffle>();

                Debug.Log(theQuaffle);

                // Fetch the target teammate
                GameObject targetTeammate = ReadFromBlackboard("teammateInGoodSpot") as GameObject;

                if (targetTeammate != null && theQuaffleGO != null && theQuaffle != null && theQuaffle.MyHolder == MyParentTree.gameObject)
                {

                    // Determine hit force vector
                    Vector3 desiredPass = targetTeammate.gameObject.transform.position - MyParentTree.gameObject.transform.position;
                    theQuaffle.Throw(desiredPass.normalized * MyParentTree.BallAddedForceMultiplier);

                    Debug.Log(string.Format("{0} should have passed to {1}.", MyParentTree.gameObject, targetTeammate));

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