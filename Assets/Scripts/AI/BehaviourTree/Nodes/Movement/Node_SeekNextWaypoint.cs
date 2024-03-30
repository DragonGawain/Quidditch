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

            // Make sure the next waypoint exists.
            if (myParentTree.ReturnNextWaypoint() == null || myParentTree.ReturnNextWaypoint() == myParentTree.ReturnCurrentWaypoint() )
            {
                myState = NodeState.FAILURE;
                return myState;
            }

            // Seek it and return running.
            Vector3 desiredVelocity = MyParentTree.MyNPCMovement.Seek(myParentTree.ReturnNextWaypoint(), MyParentTree.MyMaxSpeed);
            MyParentTree.SetVRigidbodyVelocity(desiredVelocity);
            // TODO: gooder movement behaviour. Obstacle avoidance, pathfinding, etc. - should be done

            myState = NodeState.RUNNING;
            return myState;
        }
    }
}
