using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_SeekEnemyGoalpost : Node
    {
        // CONSTRUCTORS
        public Node_SeekEnemyGoalpost(BehaviourTree parentTree) : base(parentTree) { }

        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing ArriveAtEnemyGoalpost");

            // Find the goalpost.
            GameObject theEnemyGoalpost = ReadFromBlackboard("enemyGoalpost") as GameObject;
            if (theEnemyGoalpost == null)
            {
                theEnemyGoalpost = MyParentTree.MyGroupAI.EnemyGoalpost.gameObject;
                WriteToBlackboard("enemyGoalpost", theEnemyGoalpost);
            }

            // Seek it and return running.
            Vector3 desiredVelocity = MyParentTree.MyNPCMovement.Seek(theEnemyGoalpost.transform.position, MyParentTree.MyMaxSpeed);
            MyParentTree.SetVRigidbodyVelocity(desiredVelocity);
            // TODO: gooder movement behaviour. Obstacle avoidance, pathfinding, etc. - should be done

            myState = NodeState.RUNNING;
            return myState;
        }
    }
}

