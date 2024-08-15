using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_IsTeammateHoldingQuaffle : Node
    {
        // CONSTRUCTORS
        public Node_IsTeammateHoldingQuaffle(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing IsTeammateHoldingQuaffle?");

            // Do the check.
            foreach (GameObject go in MyParentTree.MyGroupAI.chasers)
            {
                Player chaser = go.GetComponent<Player>();
                if (chaser.HasQuaffle)
                {
                    WriteToBlackboard("quaffleHolder", chaser.gameObject);

                    myState = NodeState.SUCCESS;
                    return myState;
                }
            }
            //before reformat code
            /*foreach (GroupAI chaser in MyParentTree.MyGroupAI.Chasers)
            {
                if (chaser.HasBall)
                {
                    WriteToBlackboard("quaffleHolder", chaser.gameObject);

                    myState = NodeState.SUCCESS;
                    return myState;
                }
            }*/

            //if (MyParentTree.MyGroupAI.
            //    && MyParentTree.MyGroupAI.PlayersRole == PlayerRole.CHASER)
            //{
            // TODO: check if the player is in the same team and holding the ball.
            //}

            myState = NodeState.FAILURE;
            return myState;
        }
    }
}
