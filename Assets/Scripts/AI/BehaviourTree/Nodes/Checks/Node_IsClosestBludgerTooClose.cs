using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_IsClosestBludgerTooClose : Node
    {
        // CONSTRUCTORS
        public Node_IsClosestBludgerTooClose(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing IsClosestBludgerTooClose");

            // Find the closest bludger.
            GameObject closestBludger = ReadFromBlackboard("closestBludger") as GameObject;
            (GameObject, float) closestBludgerAndDistance = MyParentTree.LocateClosestBludger(closestBludger);
            WriteToBlackboard("closestBludger", closestBludgerAndDistance.Item1);

            // Do the check.
            if (closestBludgerAndDistance.Item2 < MyParentTree.BludgerFleeDistance) // This distance threshold is set in the individual AI's root BehaviourTree script.
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

