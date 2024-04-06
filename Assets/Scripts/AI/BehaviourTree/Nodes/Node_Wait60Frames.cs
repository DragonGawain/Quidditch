using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    // Wait for a given number of frames.
    public class Node_Wait60Frames : Node
    {
        // CONSTRUCTORS
        public Node_Wait60Frames(BehaviourTree parentTree) : base(parentTree) { }


        // VARIABLES
        private int framesToWaitFor = 60;
        private int currentFrame = 0;


        // METHODS
        public override NodeState Execute()
        {
            if (currentFrame < framesToWaitFor)
            {
                currentFrame++;
                //Debug.Log(currentFrame);

                myState = NodeState.RUNNING;
                return myState;
            }
            else
            {
                currentFrame = 0;

                myState = NodeState.SUCCESS;
                return myState;
            }
        }
    }
}