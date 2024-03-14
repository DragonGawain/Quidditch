using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Seeker : BehaviourTree
{
    // VARIABLES


    // METHODS
    override protected Node PlantTheTree(BehaviourTree self)
    {
        Node rootNode = new NodeSelector(self,
                                         new List<Node> {  new NodeSequence( self,
                                                                             new List<Node> { new Node_ReachedSnitch(self), new Node_FleeBludger(this) }),
                                         new Node_SeekSnitch(this) });
        return rootNode;
    }
}
