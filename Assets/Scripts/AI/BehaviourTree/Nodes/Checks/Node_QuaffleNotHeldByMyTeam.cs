using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_QuaffleNotHeldByMyTeam : Node
    {
        // CONSTRUCTORS
        public Node_QuaffleNotHeldByMyTeam(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing IsTeammateHoldingQuaffle?");

            GameObject quaffleHolder = MyParentTree.MyGroupAI.TheQuaffle.MyHolder;
            GroupAI quaffleHolderAI = quaffleHolder != null ? quaffleHolder.GetComponent<GroupAI>() : null;

            // Do the check.
            if (quaffleHolder == null || quaffleHolderAI.GetTeam() != MyParentTree.MyGroupAI.GetTeam())
            {
                myState = NodeState.SUCCESS;
                return myState;
            }
            else
            {
                myState = NodeState.FAILURE;
                return myState;
            } 
        }
    }
}