using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_IsStanceAggressive : Node
    {
        // CONSTRUCTORS
        public Node_IsStanceAggressive(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing Node_IsStanceAggressive?");

            // Do the check.
            if (MyParentTree.MyGroupAI.GetIsInFormation() == false || MyParentTree.MyGroupAI.GetStance() == AIStance.AGGRESSIVE)
            {
                // Debug.Log(string.Format("{0}'s stance is Aggressive.", MyParentTree.gameObject));

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
