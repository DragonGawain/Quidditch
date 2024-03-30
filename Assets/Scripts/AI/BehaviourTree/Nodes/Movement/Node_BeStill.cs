using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    // Remain in place and wait.
    public class Node_BeStill : Node
    {
        // CONSTRUCTORS
        public Node_BeStill(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            myState = NodeState.RUNNING;
            return myState;
        }
    }
}
