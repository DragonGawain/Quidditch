using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_SeekNextWaypoint : Node
    {
        // CONSTRUCTORS
        public Node_SeekNextWaypoint(BehaviourTree parentTree)
            : base(parentTree) { }

        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing SeekNextWaypoint");

            Transform nextWaypoint = myParentTree.ReturnNextWaypoint();

            // Make sure the next waypoint exists.
            if (nextWaypoint == null || nextWaypoint == myParentTree.ReturnLastWaypoint()) // Should this return success instead? 
            {
                myState = NodeState.FAILURE;
                return myState;
            }

            // Seek it.
            Vector3 desiredVelocity = MyParentTree.MyNPCMovement.Seek(nextWaypoint.position, MyParentTree.MyMaxSpeed);
            MyParentTree.SetVRigidbodyVelocity(desiredVelocity);

            // Verify whether the waypoint has been reached.
            if (Vector3.Distance(MyParentTree.gameObject.transform.position, nextWaypoint.position) <= MyParentTree.WaypointHasReachedDistance)
            {
                myState = NodeState.SUCCESS;
                return myState;
            }
            else
            {
                myState = NodeState.RUNNING;
                return myState;
            }
        }
    }
}
