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

            Vector3 myPosition = MyParentTree.gameObject.transform.position;
            float myBestDistanceFromGoalpost = MyParentTree.LocateClosestEnemyGoalpost().Item2;

            // Debug.Log(MyParentTree.MyGroupAI.FriendlyChasers.Count);

            GameObject bestTeammate = null;
            float bestTeammateDistanceToGoalpost = float.MaxValue;

            // Check if any teammate is closer to the enemy goalpost than the current character
            foreach (GroupAI chaser in MyParentTree.MyGroupAI.FriendlyChasers)
            {
                // Calculate distance between myself and the teammate.
                float distanceBetweenSelfAndTeammate = Vector3.Distance(chaser.transform.position, MyParentTree.transform.position);

                // Calculate distance to enemy goalpost for the teammate
                float teammatesDistanceToGoalpost = MyParentTree.DistanceBetweenTargetAndTheirClosestEnemyGoalpost(chaser.gameObject).Item2;

                // Check if the teammate is closer to the enemy goalpost than the current character, the previous best target, and that they are distant enough from each other.
                if (teammatesDistanceToGoalpost < bestTeammateDistanceToGoalpost && teammatesDistanceToGoalpost < myBestDistanceFromGoalpost && distanceBetweenSelfAndTeammate >= 10)
                {
                    bestTeammate = chaser.gameObject;
                    bestTeammateDistanceToGoalpost = teammatesDistanceToGoalpost;
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
