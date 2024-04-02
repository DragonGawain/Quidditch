using UnityEngine;

namespace CharacterAI
{
    // Propulse the bludger away from the given target.
    public class Node_HitBludgerAwayFromTarget : Node
    {
        // CONSTRUCTORS
        public Node_HitBludgerAwayFromTarget(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            Debug.Log("Executing HitBludgerAwayFromTarget");

            // First verify that we are touching a bludger.
            if (true) // TODO: incorporate bludger check.
            {
                // Make sure it knows we are holding it.
                GameObject theBludgerGO = ReadFromBlackboard("targetBludger") as GameObject;
                Bludger theBludger = theBludgerGO.GetComponent<Bludger>();

                // Fetch protected target.
                GameObject protectedTarget = ReadFromBlackboard("protectedTarget") as GameObject;

                if (protectedTarget != null && theBludgerGO != null && theBludger != null) // && theBludger.MyHitter == MyParentTree.gameObject)
                {
                    // Determine hit force vector.
                    Vector3 desiredThrow = -1 * (protectedTarget.transform.position - MyParentTree.gameObject.transform.position);
                    theBludger.Throw(desiredThrow.normalized * MyParentTree.BallAddedForceMultiplier, MyParentTree.gameObject);

                    myState = NodeState.SUCCESS;
                    return myState;
                }
            }
            // Else, return failure.
            myState = NodeState.FAILURE;
            return myState;
        }
    }
}
