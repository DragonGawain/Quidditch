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
            Node rootNode = new NodeSelector(self,
                                             new List<Node> {  
                                                 new NodeSequence( self,
                                                     new List<Node> { 
                                                         new Node_IsClosestBludgerTooClose(this), 
                                                         new Node_IsClosestBludgerTargettingMe(this),
                                                         new Node_SetClosestBludgerAsTarget(this),
                                                         new Node_IsTargetBludgerWithinHitDistance(this),
                                                         new Node_HitBludgerAwayFromSelf(this),
                                                         new Node_Wait60Frames(this)}),
                                                 new NodeSelector(self,
                                                     new List<Node> 
                                                     {
                                                         new NodeSequence(self,
                                                            new List<Node> 
                                                            {
                                                                // Am I playing agressively?
                                                                // Determine enemy target
                                                                // Determine closest bludger to target
                                                                new Node_SeekTargetBludger(this),
                                                                // Orient bludger and target
                                                                new Node_IsTargetBludgerWithinHitDistance(this),
                                                                new Node_HitBludgerTowardsTarget(this),
                                                                new Node_Wait60Frames(this)
                                                            }),
                                                         new NodeSequence(self,
                                                            new List<Node>
                                                            {
                                                                // Am I playing defensively?
                                                                // Determine target to protect.
                                                                // Seek/tail protected target
                                                                new Node_IsClosestBludgerTooClose(this),
                                                                new Node_SeekClosestBludger(this),
                                                                new Node_SetClosestBludgerAsTarget(this),
                                                                new Node_HitBludgerAwayFromTarget(this),
                                                                new Node_Wait60Frames(this)
                                                            })
                                                     })
                                                 });
            return rootNode;
        }
    }
}
