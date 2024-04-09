using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_SeekQuaffle : Node
    {
        // CONSTRUCTORS
        public Node_SeekQuaffle(BehaviourTree parentTree)
            : base(parentTree) { }

        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing SeekQuaffle");

            // Find the quaffle.
            GameObject theQuaffle = ReadFromBlackboard("quaffle") as GameObject;
            if (theQuaffle == null)
            {
                theQuaffle = MyParentTree.MyGroupAI.TheQuaffle.gameObject;
                WriteToBlackboard("quaffle", theQuaffle);
            }

            // Seek it and return running.
            Vector3 desiredVelocity = MyParentTree.MyNPCMovement.Seek(theQuaffle.transform.position, MyParentTree.MyMaxSpeed);
            MyParentTree.SetVRigidbodyVelocity(desiredVelocity);
            // TODO: gooder movement behaviour. Obstacle avoidance, pathfinding, etc. - should be done

            myState = NodeState.SUCCESS;
            return myState;
        }
    }
}

