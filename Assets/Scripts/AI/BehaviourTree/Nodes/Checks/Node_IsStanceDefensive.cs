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
            if (MyParentTree.MyGroupAI.GetIsInFormation())
            {
                Debug.Log(string.Format("{0}'s stance is Defensive.", MyParentTree.gameObject));

                // Let's just set the protectedTarget here for ease of use.
                GroupAI headChaser = MyParentTree.MyGroupAI.GetMyFormation().GetChasers()[0];

                if (headChaser != null)
                {
                    GameObject headChaserGO = headChaser.gameObject;

                    //WriteToBlackboard("protectedTarget", MyParentTree.MyGroupAI.GetMyFormation().GetChasers()[0].gameObject);
                    myState = NodeState.SUCCESS;
                    return myState;
                }
            }
            // Else
            myState = NodeState.FAILURE;
            return myState;
        }
    }
}

