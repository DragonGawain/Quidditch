using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Chaser : BehaviourTree
{
    // VARIABLES


    // METHODS
    override protected Node PlantTheTree(BehaviourTree self)
    {
        Node rootNode = new NodeSelector(self,
                                         new List<Node> {  new NodeSequence( self,
                                                                             new List<Node> { new Node_IsClosestBludgerTooClose(self), new Node_FleeClosestBludger(this) }),
                                                           new NodeSequence(self,
                                                                             new List<Node> { new Node_AmIHoldingQuaffle(self), new Node_SeekEnemyGoalpost(this) }),
                                                           new Node_SeekQuaffle(this) });
        return rootNode;
    }
}
