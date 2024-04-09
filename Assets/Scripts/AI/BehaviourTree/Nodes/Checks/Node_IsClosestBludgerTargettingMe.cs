using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_IsClosestBludgerTargettingMe : Node
    {
        // CONSTRUCTORS
        public Node_IsClosestBludgerTargettingMe(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing IsClosestBludgerTargettingMe");

            // Find the closest bludger.
            GameObject closestBludger = ReadFromBlackboard("closestBludger") as GameObject;
            (GameObject, float) closestBludgerAndDistance = MyParentTree.LocateClosestBludger(closestBludger);
            WriteToBlackboard("closestBludger", closestBludgerAndDistance.Item1);

            Bludger closestBludgerBludger = closestBludgerAndDistance.Item1.GetComponent<Bludger>();

            // Do the check.
            if (closestBludgerBludger != null && closestBludgerBludger.Target != null && closestBludgerBludger.Target.gameObject == MyParentTree.gameObject)
            { // TODO: Test whether this works as intended.
                //Debug.Log(string.Format("The Bludger {0} is targeting {1}.", closestBludgerBludger.gameObject, closestBludgerBludger.Target));
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