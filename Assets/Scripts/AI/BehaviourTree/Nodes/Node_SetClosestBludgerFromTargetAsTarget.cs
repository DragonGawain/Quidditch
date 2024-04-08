using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_SetClosestBludgerFromTargetAsTarget : Node
    {
        // CONSTRUCTORS
        public Node_SetClosestBludgerFromTargetAsTarget(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing Node_SetClosestBludgerFromTargetAsTarget");

            // Find the closest bludger.
            GameObject target = ReadFromBlackboard("target") as GameObject;
            if (target != null)
            {
                GameObject closestBludger = MyParentTree.LocateClosestBludgerToTarget(target).Item1;
                //WriteToBlackboard("closestBludger", closestBludger);

                // Set and return.
                if (closestBludger)
                {
                    WriteToBlackboard("targetBludger", closestBludger);
                    myState = NodeState.SUCCESS;
                    return myState;
                }
            }
            // Else, return failure.
            myState = NodeState.FAILURE;
            return myState;
        }
    }
}
