using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    // "?"
    public class NodeSelector : NodeBranch
    {
        // CONSTRUCTORS
        public NodeSelector(BehaviourTree parentTree, List<Node> childNodes = null) : base(parentTree, childNodes) { }


        // METHODS.
        override public NodeState Execute()
        {
            foreach (Node child in myChildNodes)
            {
                switch (child.Execute())
                {
                    case NodeState.SUCCESS:
                        myState = NodeState.SUCCESS;
                        return myState;

                    case NodeState.FAILURE:
                        continue;

                    case NodeState.RUNNING:
                        myState = NodeState.RUNNING;
                        return myState;

                    default:
                        continue;
                }
            }

            // Should they all fail, return false.
            myState = NodeState.FAILURE;
            return myState;
        }
    }
}
