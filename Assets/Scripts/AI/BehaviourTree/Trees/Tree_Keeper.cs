using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Tree_Keeper : BehaviourTree
    {
        // VARIABLES


        // METHODS
        override protected Node PlantTheTree(BehaviourTree self)
        {
            Node rootNode = new NodeSelector(self,
                                                new List<Node> {
                                                    new NodeSequence(self,
                                                        new List<Node> 
                                                        {
                                                            new Node_IsClosestBludgerTooClose(this),
                                                            new Node_FleeClosestBludger(this)
                                                        }
                                                    ),
                                                    new NodeSequence(self,
                                                        new List<Node>
                                                        {
                                                            new Node_IsQuaffleTooClose(this),
                                                            new NodeSelector(self,
                                                                new List<Node>
                                                                {
                                                                    new NodeSequence(self,
                                                                        new List<Node>
                                                                        {
                                                                            new Node_IsQuaffleFree(this),
                                                                            new Node_SeekQuaffle(this)
                                                                        }),
                                                                    new NodeSequence(self,
                                                                        new List<Node>
                                                                        {
                                                                            // Estimate enemy's target
                                                                            // Seek enemy's target ring
                                                                            // Guard target ring
                                                                        })
                                                                })
                                                        }),
                                                    new NodeSequence(self,
                                                        new List<Node> 
                                                        {
                                                            new Node_SeekNextWaypoint(this),
                                                            new Node_IncreateCurrentWaypointLooping(this)
                                                        }
                                                    )
                                                });
            return rootNode;
        }
    }
}