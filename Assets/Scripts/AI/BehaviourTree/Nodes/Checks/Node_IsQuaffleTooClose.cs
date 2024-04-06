using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_IsQuaffleTooClose : Node
    {
        // CONSTRUCTORS
        public Node_IsQuaffleTooClose(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing IsEnemyQuaffleTooClose");

            GameObject theQuaffle = MyParentTree.MyGroupAI.TheQuaffle.gameObject;
            float distanceFromQuaffle = Vector3.Distance(MyParentTree.gameObject.transform.position, theQuaffle.transform.position);

            // Do the check.
            if (distanceFromQuaffle < MyParentTree.QuaffleConcernDistance) // This distance threshold is set in the individual AI's root BehaviourTree script.
            {
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