using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Vector3 desiredVelocity = Vector3.ClampMagnitude(
            MyParentTree.MyNPCMovement.KinematicSeek(
                theSnitch.transform.position,
                MyParentTree.Acceleration
            ) + MyParentTree.GetRigidbody().velocity,
            MyParentTree.MyMaxSpeed
        );
        MyParentTree.SetVelocity(desiredVelocity);
        // MyParentTree.gameObject.transform.position += desiredVelocity * Time.deltaTime;
        // TODO: gooder movement behaviour. Obstacle avoidance, pathfinding, etc. - should be done

        myState = NodeState.RUNNING;
        return myState;
    }
}
