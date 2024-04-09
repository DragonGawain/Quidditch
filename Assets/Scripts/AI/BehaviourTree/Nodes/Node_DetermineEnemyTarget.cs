using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Node_DetermineEnemyTarget : Node
    {
        // CONSTRUCTORS
        public Node_DetermineEnemyTarget(BehaviourTree parentTree) : base(parentTree) { }


        // METHODS
        public override NodeState Execute()
        {
            //Debug.Log("Executing Node_DetermineEnemyTarget");

            // Is any enemy holding the Quaffle?
            GameObject theQuafflesHolder = MyParentTree.MyGroupAI.TheQuaffle.MyHolder;
            if (theQuafflesHolder != null)
            {
                GroupAI theQuaffleHoldersAI = theQuafflesHolder.GetComponent<GroupAI>();
                if (theQuaffleHoldersAI != null && theQuaffleHoldersAI.Team != MyParentTree.MyGroupAI.Team)
                {

                    WriteToBlackboard("attackTarget", theQuafflesHolder);

                    Debug.LogWarning(string.Format("{0}'s chosen attack target is {1}", MyParentTree.gameObject, theQuafflesHolder));

                    myState = NodeState.SUCCESS;
                    return myState;
                }
            } // Else,
            WriteToBlackboard("attackTarget", MyParentTree.MyGroupAI.GetEnemySeeker());

            Debug.LogWarning(string.Format("{0}'s chosen attack target is {1}", MyParentTree.gameObject, MyParentTree.MyGroupAI.GetEnemySeeker()));

            myState = NodeState.SUCCESS;
            return myState;
        }
    }
}