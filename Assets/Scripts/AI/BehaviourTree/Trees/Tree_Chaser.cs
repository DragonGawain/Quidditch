using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Tree_Chaser : BehaviourTree
    {
        // VARIABLES


        // METHODS
        override protected Node PlantTheTree(BehaviourTree self)
        {
            Node rootNode = new NodeSelector(
                self,
                new List<Node>
                {
                    new NodeSequence(
                        self,
                        new List<Node>
                        {
                            new Node_IsClosestBludgerTooClose(this),
                            new Node_FleeClosestBludger(this)
                        }
                    ),
                    new NodeSelector(
                        self,
                        new List<Node>
                        {
                            new NodeSequence(
                                self,
                                new List<Node>
                                {
                                    new Node_AmIHoldingQuaffle(this),
                                    new NodeSelector(
                                        self,
                                        new List<Node>
                                        {
                                            // TODO: determine what goalpost to target
                                            new NodeSequence(
                                                self,
                                                new List<Node>
                                                {
                                                    // Is teammate in a good spot for a pass?
                                                    new Node_CheckTeammateForPass(this),
                                                    // Pass to teammate
                                                    new Node_PassQuaffleToTeammate(this),
                                                    new Node_Wait60Frames(this)
                                                }
                                            ),
                                            new NodeSequence(
                                                self,
                                                new List<Node>
                                                {
                                                    // TODO: determine best goalpost.
                                                    new Node_HasReachedEnemyGoalpost(this),
                                                    new Node_ThrowQuaffleAtGoalpost(this),
                                                    new Node_Wait60Frames(this)
                                                }
                                            ),
                                            new Node_SeekEnemyGoalpost(this)
                                        }
                                    )
                                }
                            ),
                            new NodeSequence(
                                self,
                                new List<Node>
                                {
                                    new Node_IsTeammateHoldingQuaffle(this),
                                    new Node_SeekFormationSpot(this)
                                }
                            ),
                            new NodeSelector(
                                self,
                                new List<Node>
                                {
                                    new NodeSequence(
                                        self,
                                        new List<Node>
                                        {
                                            new Node_IsEnemyHoldingQuaffle(this),
                                            // Seek the point between the enemy and their target
                                        }
                                    ),
                                    new Node_SeekQuaffle(this)
                                }
                            )
                        }
                    )
                }
            );
            return rootNode;
        }
    }
}
