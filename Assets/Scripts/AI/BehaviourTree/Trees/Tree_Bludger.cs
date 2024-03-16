using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Bludger : BehaviourTree
{
    // VARIABLES


    // METHODS
    override protected Node PlantTheTree(BehaviourTree self)
    {
        Node rootNode = new NodeSelector(self,
                                         new List<Node> {  new NodeSequence( self,
                                                                             new List<Node> { new Node_IsClosestBludgerTooClose(self), new Node_FleeClosestBludger(this) }),
                                         new Node_SeekClosestBludger(this) });
        return rootNode;
    }
}
