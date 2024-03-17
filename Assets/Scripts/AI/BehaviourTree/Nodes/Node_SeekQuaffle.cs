using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_SeekQuaffle : Node
{
    // CONSTRUCTORS
    public Node_SeekQuaffle(BehaviourTree parentTree)
        : base(parentTree) { }

    // METHODS
    public override NodeState Execute()
    {
        //Debug.Log("Executing SeekQuaffle");

        // Find the snitch.
        GameObject theQuaffle = ReadFromBlackboard("quaffle") as GameObject;
        if (theQuaffle == null)
        {
            theQuaffle = MyParentTree.MyGroupAI.TheQuaffle.gameObject;
            WriteToBlackboard("quaffle", theQuaffle);
        }

        // Seek it and return running.
        // Vector3 desiredVelocity = MyParentTree.MyNPCMovement.KinematicSeek(theQuaffle.transform.position, MyParentTree.MyMaxSpeed);
        // MyParentTree.gameObject.transform.position += desiredVelocity * Time.deltaTime;
        Vector3 desiredVelocity = Vector3.ClampMagnitude(
            MyParentTree.MyNPCMovement.KinematicSeek(
                theQuaffle.transform.position,
                MyParentTree.Acceleration
            ) + MyParentTree.GetRigidbody().velocity,
            MyParentTree.MyMaxSpeed
        );
        MyParentTree.SetVelocity(desiredVelocity);
        // TODO: gooder movement behaviour. Obstacle avoidance, pathfinding, etc. - should be done

        // TODO: alternate "Arrive at quaffle" node.

        myState = NodeState.RUNNING;
        return myState;
    }
}
