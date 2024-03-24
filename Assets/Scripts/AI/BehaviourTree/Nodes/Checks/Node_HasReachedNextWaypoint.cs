using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_HasReachedNextWaypoint : Node
    {
        // CONSTRUCTORS
        public Node_HasReachedNextWaypoint(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            Debug.Log("Executing HasReachedNextWaypoint?");

            // Do the check.
            if (Vector3.Distance(MyParentTree.gameObject.transform.position, MyParentTree.ReturnNextWaypoint()) <= MyParentTree.GenericHasReachedDistance)
            {
                // If the waypoint has been reached, increase it.
                MyParentTree.IncreaseCurrentWaypointIndex();

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