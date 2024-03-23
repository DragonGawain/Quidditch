using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_HasReachedQuaffle : Node
    {
        // CONSTRUCTORS
        public Node_HasReachedQuaffle(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            Debug.Log("Executing HasReachedQuaffle?");

            // Find the quaffle.
            GameObject theQuaffle = ReadFromBlackboard("quaffle") as GameObject;
            if (theQuaffle == null)
            {
                theQuaffle = MyParentTree.MyGroupAI.TheQuaffle.gameObject;
                WriteToBlackboard("quaffle", theQuaffle);
            }

            // Do the check.
            // To do: set the distance threshold to something legit.
            if (Vector3.Distance(MyParentTree.gameObject.transform.position, theQuaffle.transform.position) <= 1f)
            {
                myState = NodeState.SUCCESS;
            }
            else
            {
                myState = NodeState.FAILURE;
            }
            return myState;
        }
    }
}

