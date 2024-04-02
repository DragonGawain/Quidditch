using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_IsEnemyHoldingQuaffle : Node
    {
        // CONSTRUCTORS
        public Node_IsEnemyHoldingQuaffle(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            Debug.Log("Executing IsEnemyHoldingQuaffle?");

            // Do the check.
            GameObject quafflesCurrentHolderGO = MyParentTree.MyGroupAI.TheQuaffle.MyHolder;
            GroupAI quafflesCurrentHolder = quafflesCurrentHolderGO.GetComponent<GroupAI>();

            if (quafflesCurrentHolderGO != null && quafflesCurrentHolder != null && quafflesCurrentHolder.GetTeam() != myParentTree.MyGroupAI.GetTeam())
            {
                WriteToBlackboard("enemyHoldingQuaffle", quafflesCurrentHolderGO);

                myState = NodeState.SUCCESS;
                return myState;
            }

            // TODO: Check if the enemy player is holding the quaffle.

            WriteToBlackboard("enemyHoldingQuaffle", null);

            myState = NodeState.FAILURE;
            return myState;
        }
    }
}