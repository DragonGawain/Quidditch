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

            GameObject go = MyParentTree.MyGroupAI.quaffle;
            Quaffle q = go.GetComponent<Quaffle>();
            GroupAI quaffleHolderAI = q.Holder != null ? q.Holder.GetComponent<GroupAI>() : null;

            // Do the check.
            if (q.Holder == null || quaffleHolderAI.Team != MyParentTree.MyGroupAI.Team)
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