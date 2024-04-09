using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    // Might delete if SeekTarget fulfills the same role.
    public class Node_SeekEnemyGoalpost : Node
    {
        // CONSTRUCTORS
        public Node_SeekEnemyGoalpost(BehaviourTree parentTree) : base(parentTree) { }

        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing SeekEnemyGoalpost");

            // Find the goalpost.
            GameObject targetEnemyGoalpost = ReadFromBlackboard("enemyGoalpost") as GameObject;
            if (targetEnemyGoalpost == null)
            {
                targetEnemyGoalpost = MyParentTree.LocateClosestEnemyGoalpost().Item1;
                WriteToBlackboard("enemyGoalpost", targetEnemyGoalpost);
            }

            // Seek it and return running.
            Vector3 desiredVelocity = MyParentTree.MyNPCMovement.Seek(targetEnemyGoalpost.transform.position, MyParentTree.MyMaxSpeed);
            MyParentTree.SetVRigidbodyVelocity(desiredVelocity);

            Debug.Log(MyParentTree.gameObject + " is seeking the enemy Goalpost.");

            myState = NodeState.SUCCESS;
            return myState;
        }
    }
}

