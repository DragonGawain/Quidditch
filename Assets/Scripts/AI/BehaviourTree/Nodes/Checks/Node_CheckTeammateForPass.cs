using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_CheckTeammateForPass : Node
    {
        // CONSTRUCTORS
        public Node_CheckTeammateForPass(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            Debug.Log("Executing CheckTeammateForPass");

            Transform enemyGoalpost = MyParentTree.MyGroupAI.EnemyGoalpost.transform;
            Vector3 myPosition = MyParentTree.gameObject.transform.position;

            Debug.Log(MyParentTree.MyGroupAI.FriendlyChasers.Count);

            // Check if any teammate is closer to the enemy goalpost than the current character
            foreach (GroupAI chaser in MyParentTree.MyGroupAI.FriendlyChasers)
            {

                // Calculate distance to enemy goalpost for the teammate
                float distanceToGoalpost = Vector3.Distance(chaser.transform.position, enemyGoalpost.position);
                float myDistanceToGoalpost = Vector3.Distance(myPosition, enemyGoalpost.position);

                // Check if the teammate is closer to the enemy goalpost than the current character
                if (distanceToGoalpost < myDistanceToGoalpost)
                {
                    // Update blackboard with information about the teammate in a good spot for a pass
                    WriteToBlackboard("teammateInGoodSpot", chaser.gameObject);

                    myState = NodeState.SUCCESS;
                    return myState;
                }
            }

            // No teammate found in a good spot for a pass
            myState = NodeState.FAILURE;
            return myState;
        }
    }
}
