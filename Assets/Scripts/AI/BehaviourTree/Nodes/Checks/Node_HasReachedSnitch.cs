using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_HasReachedSnitch : Node
    {
        // CONSTRUCTORS
        public Node_HasReachedSnitch(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            Debug.Log("Executing HasReachedSnitch?");

            // Find the snitch.
            GameObject theSnitch = ReadFromBlackboard("snitch") as GameObject;
            if (theSnitch == null)
            {
                theSnitch = MyParentTree.MyGroupAI.TheSnitch.gameObject;
                WriteToBlackboard("snitch", theSnitch);
            }

            // Do the check.
            // The distance threshold is set in the individual AI's root BehaviourTree script.
            if (Vector3.Distance(MyParentTree.gameObject.transform.position, theSnitch.transform.position) <= MyParentTree.GenericHasReachedDistance)
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

