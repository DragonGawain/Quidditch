using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_FleeBludger : Node
{
    // VARIABLES
    int counter = 0;

    

    // CONSTRUCTORS
    public Node_FleeBludger(BehaviourTree parentTree) : base(parentTree) { }


    // METHODS
    public override NodeState Execute()
    {
        Debug.Log("Executing FleeBludger");

        // Find the snitch.
        GameObject closestBludger = ReadFromBlackboard("bludger") as GameObject;
        if (closestBludger == null)
        {
            closestBludger = GameObject.FindGameObjectWithTag("bludger");
        }

        // Verify whether enough time has passed.
        if (counter >= 100f)
        {
            Debug.Log("FleeBludger completed.");

            counter = 0;

            myState = NodeState.SUCCESS;
            return myState;
        }

        // Else, keep fleeing, increase the counter and return running.
        Vector3 desiredVelocity = MyParentTree.MyNPCMovement.KinematicFlee(closestBludger.transform.position, 1f);
        MyParentTree.gameObject.transform.position += desiredVelocity * Time.deltaTime;
        // To do: gooder movement behaviour. Obstacle avoidance, pathfinding, etc.

        counter++;
        // Debug.Log(counter);

        myState = NodeState.RUNNING;
        return myState;
    }
}
