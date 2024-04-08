using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_IsQuaffleFree : Node
    {
        // CONSTRUCTORS
        public Node_IsQuaffleFree(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing IsQuaffleFree?");

            // Do the check.
            GameObject quafflesCurrentHolderGO = MyParentTree.MyGroupAI.TheQuaffle.MyHolder;

            if (quafflesCurrentHolderGO == null)
            {
                ClearFromBlackboard("quaffleHolder");
                ClearFromBlackboard("enemyHoldingQuaffle");

                myState = NodeState.SUCCESS;
                return myState;
            }

            myState = NodeState.FAILURE;
            return myState;
        }
    }
}