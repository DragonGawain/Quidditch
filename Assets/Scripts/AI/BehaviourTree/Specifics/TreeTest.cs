using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTest : Tree
{
    // METHODS
    override protected Node PlantTheTree()
    {
        Node rootNode = new SelectorNode(
            new List<Node>
            {
                new SequenceNode(
                    new List<Node>
                    {
                        new NodeTest_CoinFlip(),
                        new NodeTest_DelayedAction()
                    }),

                new NodeTest_InstantAction()
            });

        return rootNode;
    }
}
