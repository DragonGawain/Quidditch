using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_IncreateCurrentWaypoint : Node
    {
        // CONSTRUCTORS
        public Node_IncreateCurrentWaypoint(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing IncreateCurrentWaypoint");

            // Increase the waypoint counter.
            MyParentTree.UpdateNextWaypointIndex();

            myState = NodeState.SUCCESS;
            return myState;
        }
    }
}