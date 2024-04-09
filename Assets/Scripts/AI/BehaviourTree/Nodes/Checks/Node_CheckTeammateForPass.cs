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

            // Debug.Log(MyParentTree.MyGroupAI.FriendlyChasers.Count);

            GameObject bestTeammate = null;
            float bestDistanceToGoalpost = float.MaxValue;

            // Check if any teammate is closer to the enemy goalpost than the current character
            foreach (GroupAI chaser in MyParentTree.MyGroupAI.FriendlyChasers)
            {
                // Calculate distance between myself and the teammate.
                float distanceBetweenSelfAndTeammate = Vector3.Distance(chaser.transform.position, MyParentTree.transform.position);

                //Debug.Log(string.Format("Distance between {0} and {1}: {2}", MyParentTree.gameObject, chaser.gameObject, distanceBetweenSelfAndTeammate));

                // Calculate distance to enemy goalpost for the teammate
                float distanceToGoalpost = Vector3.Distance(chaser.transform.position, enemyGoalpost.position);
                float myDistanceToGoalpost = Vector3.Distance(myPosition, enemyGoalpost.position);

                // Check if the teammate is closer to the enemy goalpost than the current character, the previous best target, and that they are distant enough from each other.
                if (distanceToGoalpost < bestDistanceToGoalpost && distanceToGoalpost < myDistanceToGoalpost && distanceBetweenSelfAndTeammate >= 10)
                {
                    bestTeammate = chaser.gameObject;
                    bestDistanceToGoalpost = distanceToGoalpost;
                }
            }
            if (bestTeammate != null)
            {
                Debug.Log("Best teammate for pass: " + bestTeammate);

                // Update blackboard with information about the teammate in a good spot for a pass
                WriteToBlackboard("teammateInGoodSpot", bestTeammate);

                myState = NodeState.SUCCESS;
                return myState;
            }
            // No teammate found in a good spot for a pass
            Debug.Log("No good teammate was found for a pass.");

            myState = NodeState.FAILURE;
            return myState;
        }
    }
}
