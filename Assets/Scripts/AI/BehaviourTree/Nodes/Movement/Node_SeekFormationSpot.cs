using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_SeekFormationSpot : Node
    {
        // CONSTRUCTORS
        public Node_SeekFormationSpot(BehaviourTree parentTree) : base(parentTree) { }

        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing Node_SeekFormationSpot");

            // Verify the formation is valid.
            if (MyParentTree.MyGroupAI.GetIsInFormation() == false)
            {
                Debug.Log(string.Format("{0} has no valid formation.", MyParentTree.gameObject));

                myState = NodeState.FAILURE;
                return myState;
            }

            // Fetch the target position.
            Vector3 targetPosition = MyParentTree.MyGroupAI.GetFormationPosition();

            // Seek it and return running.
            Vector3 desiredVelocity = MyParentTree.MyNPCMovement.Seek(targetPosition, MyParentTree.MyMaxSpeed);
            MyParentTree.SetVRigidbodyVelocity(desiredVelocity);

            // Should this be success?
            myState = NodeState.RUNNING;
            return myState;
        }
    }
}
