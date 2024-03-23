using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_SeekSnitch : Node
    {
        // CONSTRUCTORS
        public Node_SeekSnitch(BehaviourTree parentTree)
            : base(parentTree) { }

        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing SeekSnitch");

            // Find the snitch.
            GameObject theSnitch = ReadFromBlackboard("snitch") as GameObject;
            if (theSnitch == null)
            {
                theSnitch = MyParentTree.MyGroupAI.TheSnitch.gameObject;
                WriteToBlackboard("snitch", theSnitch);
            }

            // Seek it and return running.
            Vector3 desiredVelocity = MyParentTree.MyNPCMovement.Seek(theSnitch.transform.position, MyParentTree.MyMaxSpeed);
            MyParentTree.SetVelocity(desiredVelocity);
            // TODO: gooder movement behaviour. Obstacle avoidance, pathfinding, etc. - should be done



            myState = NodeState.RUNNING;
            return myState;
        }
    }
}

