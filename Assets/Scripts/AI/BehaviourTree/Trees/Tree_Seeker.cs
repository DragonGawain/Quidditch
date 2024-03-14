using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Seeker : BehaviourTree
{
    // VARIABLES


    // METHODS
    override protected Node PlantTheTree(BehaviourTree self)
    {
        Node rootNode = new NodeSequence(self,
                                         new List<Node> {  new Node_SeekSnitch(this),
                                                           new Node_FleeBludger(this)});
        return rootNode;
    }
}
