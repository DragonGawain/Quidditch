using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Tree_Beater : BehaviourTree
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
                            //new Node_IsClosestBludgerTooClose(this),
                            new Node_IsClosestBludgerTargettingMe(this),
                            new Node_SetClosestBludgerAsTarget(this),
                            new Node_IsTargetBludgerWithinHitDistance(this),
                            new Node_HitBludgerAwayFromSelf(this),
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
                                    new Node_IsStanceAggressive(this),
                                    new Node_DetermineEnemyTarget(this),
                                    new Node_SetClosestBludgerFromTargetAsTarget(this),
                                    new Node_SeekTargetBludger(this),
                                    new Node_IsTargetBludgerWithinHitDistance(this),
                                    new Node_HitBludgerTowardsTarget(this),
                                }
                            ),
                            new NodeSequence(
                                self,
                                new List<Node>
                                {
                                    new Node_IsStanceDefensive(this),
                                    new NodeSelector(self,
                                        new List<Node>
                                        {
                                            new NodeSequence(self,
                                                new List<Node>
                                                {
                                                    new Node_IsClosestBludgerTooClose(this),
                                                    new Node_SetClosestBludgerAsTarget(this),
                                                    new Node_SeekTargetBludger(this),
                                                    new Node_IsTargetBludgerWithinHitDistance(this),
                                                    new Node_HitBludgerAwayFromTarget(this),
                                                }),
                                            new Node_SeekFormationSpot(this)
                                        })
                                })
                        })
                });
            return rootNode;
        }
    }
}
