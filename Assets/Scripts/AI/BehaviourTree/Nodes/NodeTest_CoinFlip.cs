using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTest_CoinFlip : Node
{
    // CONSTRUCTORS
    public NodeTest_CoinFlip(BehaviourTree parentTree) : base(parentTree) { }


    // METHODS
    public override NodeState Execute()
    {
        Debug.Log("The test behaviour tree is executing a coin flip!");

        myState = RandomResult(0.5f);
        Debug.Log(string.Format("The result of the coin flip is {0}.", myState));

        return myState;
    }
}
