using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_IncreateCurrentWaypointLooping : Node
    {
        // CONSTRUCTORS
        public Node_IncreateCurrentWaypointLooping(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            Debug.Log("Executing IncreateCurrentWaypointLooping");

            // Increase the waypoint counter.
            MyParentTree.UpdateNextWaypointIndex(true);

            myState = NodeState.SUCCESS;
            return myState;
        }
    }
}