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
            // The distance threshold is set in the individual AI's root BehaviourTree script.
            if (Vector3.Distance(MyParentTree.gameObject.transform.position, theQuaffle.transform.position) <= MyParentTree.GenericHasReachedDistance)
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

