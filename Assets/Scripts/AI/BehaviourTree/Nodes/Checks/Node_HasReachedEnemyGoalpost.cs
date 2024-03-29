using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    // TODO: erase if it can be handled by a generic node + a value writen into the blackboard.
    public class Node_HasReachedEnemyGoalpost : Node
    {
        // CONSTRUCTORS
        public Node_HasReachedEnemyGoalpost(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            Debug.Log("Executing HasReachedEnemyGoalpost?");

            // Find the goalpost.
            GameObject theEnemyGoalpost = ReadFromBlackboard("enemyGoalpost") as GameObject;
            if (theEnemyGoalpost == null)
            {
                theEnemyGoalpost = MyParentTree.MyGroupAI.EnemyGoalpost.gameObject;
                WriteToBlackboard("enemyGoalpost", theEnemyGoalpost);
            }

            // Do the check.
            // TODO: set the distance threshold to something legit.
            if (Vector3.Distance(MyParentTree.gameObject.transform.position, theEnemyGoalpost.transform.position) <= 10f)
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

