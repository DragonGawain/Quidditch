using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTest_DelayedAction : Node
{
    // VARIABLES
    int counter = 0;



    // CONSTRUCTORS
    public NodeTest_DelayedAction(BehaviourTree parentTree) : base(parentTree) { }


    // METHODS
    public override NodeState Execute()
    {
        if (counter >= 1000)
        {
            Debug.Log("The test behaviour tree is executing a delayed action!");

            counter = 0;

            myState = NodeState.SUCCESS;
            return myState;
        }

        counter++;
        //Debug.Log(counter);

        myState = NodeState.RUNNING;
        return myState;
    }
}
