using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_IsStanceDefensive : Node
    {
        // CONSTRUCTORS
        public Node_IsStanceDefensive(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing Node_IsStanceDefensive?");

            // Do the check.
            if (MyParentTree.MyGroupAI.GetStance() == AIStance.DEFENSIVE)
            {
                // Debug.Log(string.Format("{0}'s stance is Defensive.", MyParentTree.gameObject));

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

