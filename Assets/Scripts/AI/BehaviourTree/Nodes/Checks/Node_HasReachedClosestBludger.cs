using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_HasReachedClosestBludger : Node
    {
        // CONSTRUCTORS
        public Node_HasReachedClosestBludger(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            Debug.Log("Executing HasReachedClosestBludger?");

            // Find the snitch.
            GameObject closestBludger = ReadFromBlackboard("closestBludger") as GameObject;
            closestBludger = MyParentTree.LocateClosestBludger(closestBludger).Item1;
            WriteToBlackboard("closestBludger", closestBludger);

            // Do the check.
            // To do: set the distance threshold to something legit.
            if (Vector3.Distance(MyParentTree.gameObject.transform.position, closestBludger.transform.position) <= MyParentTree.GenericHasReachedDistance)
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
