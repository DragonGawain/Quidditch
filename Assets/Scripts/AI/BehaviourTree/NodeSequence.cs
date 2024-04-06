using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    // "->"
    public class NodeSequence : NodeBranch
    {
        // CONSTRUCTORS
        public NodeSequence(BehaviourTree parentTree, List<Node> childNodes = null) : base(parentTree, childNodes) { }


        // METHODS.
        override public NodeState Execute()
        {
            //bool anyChildIsRunning = false;

            foreach (Node child in myChildNodes)
            {
                switch (child.Execute())
                {
                    case NodeState.SUCCESS:
                        continue;

                    case NodeState.FAILURE:
                        myState = NodeState.FAILURE;
                        return myState;

                    case NodeState.RUNNING:
                        //anyChildIsRunning = true;
                        //continue;
                        myState = NodeState.RUNNING;
                        return myState;

                    default:
                        myState = NodeState.SUCCESS;
                        return myState;
                }
            }

            // Should they all succeed, return true.
            //myState = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            myState = NodeState.SUCCESS;
            return myState;
        }
    }
}