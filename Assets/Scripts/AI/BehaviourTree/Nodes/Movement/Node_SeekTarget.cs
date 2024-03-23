using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    // Seek a generic target (registered by that name on the blackboard).
    public class Node_SeekTarget : Node
    {
        // CONSTRUCTORS
        public Node_SeekTarget(BehaviourTree parentTree) : base(parentTree) { }

        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing Node_SeekTarget");

            // Find the target bludger.
            GameObject target = ReadFromBlackboard("target") as GameObject;
            if (target == null)
            {
                // We assume it has already been set
                myState = NodeState.FAILURE;
                return myState;
            }

            // Seek it and return running.
            Vector3 desiredVelocity = MyParentTree.MyNPCMovement.Seek(target.transform.position, MyParentTree.MyMaxSpeed);
            MyParentTree.SetVRigidbodyVelocity(desiredVelocity);
            // To do: gooder movement behaviour. Obstacle avoidance, pathfinding, etc. - should be done

            myState = NodeState.RUNNING;
            return myState;
        }
    }
}
