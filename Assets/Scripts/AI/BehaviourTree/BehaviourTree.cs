using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public abstract class BehaviourTree : MonoBehaviour
    {
        // VARIABLES
        [SerializeField]
        private Node myRootNode = null;

        // References.
        [SerializeField]
        protected NPCMovement myNPCMovement;
        public NPCMovement MyNPCMovement
        {
            get { return myNPCMovement; }
        }

        [SerializeField]
        protected GroupAI myGroupAI;
        public GroupAI MyGroupAI
        {
            get { return myGroupAI; }
        }

        protected Rigidbody myRigidbody;
        public Rigidbody GetRigidbody()
        {
            return myRigidbody;
        }
        public void SetVRigidbodyVelocity(Vector3 desiredVelocity)
        {
            myRigidbody.velocity = Vector3.ClampMagnitude(desiredVelocity + myRigidbody.velocity, myMaxSpeed);
        }

        // Per role or character information.
        [SerializeField, Range(0, 15)] // Edit these ranges if needed.
        protected float myMaxSpeed = 5f;
        public float MyMaxSpeed
        {
            get { return myMaxSpeed; }
        }

        [SerializeField, Range(0, 5)] // At what distance does this character consider a target as having been reached?
        protected float genericHasReachedDistance = 1f;
        public float GenericHasReachedDistance
        {
            get { return genericHasReachedDistance; }
        }

        [SerializeField, Range(0, 50)] // From what distance should this character follow a target?
        protected float followHasReachedDistance = 5f;
        public float FollowHasReachedDistance
        {
            get { return followHasReachedDistance; }
        }

        [SerializeField, Range(1, 50)] // At what distance from a bludger should the character begin to flee it?
        protected float bludgerFleeDistance = 5f;
        public float BludgerFleeDistance
        {
            get { return bludgerFleeDistance; }
        }

        [SerializeField, Range(1, 50)] // At what distance should the character feel concerned about an enemy held quaffle?
        protected float quaffleConcernDistance = 10f;
        public float QuaffleConcernDistance
        {
            get { return quaffleConcernDistance; }
        }

        [SerializeField, Range(0, 5)] // Modulates the force of a throw on hit ball.
        protected float ballAddedForceMultiplier = 1f;
        public float BallAddedForceMultiplier
        {
            get { return ballAddedForceMultiplier; }
        }



        // METHODS
        protected abstract Node PlantTheTree(BehaviourTree self);

        protected void Awake()
        {
            // Collect other references.
            myNPCMovement = gameObject.GetComponent<NPCMovement>();
            myGroupAI = gameObject.GetComponent<GroupAI>();
            myRigidbody = GetComponent<Rigidbody>(); // Extra note from Craig: you don't need to say gameObject.GetComponent<>, the gameObject is implied) // Roxane: I like being explicit my dude :P
        }

        protected void Start()
        {
            BehaviourTree currentBehaviourTree = this;
            myRootNode = PlantTheTree(this);
        }

        // FixedUpdate cause it triggers physics things (though the check to see if the physics should be done or not should be in Update, so this is a bit of an edge case. Still gonna leave it as FU though cause physics)
        private void FixedUpdate()
        {
            if (myRootNode != null)
            {
                myRootNode.Execute();
            }
        }

        // UTILITY METHODS
        public (GameObject, float) LocateClosestBludger(GameObject closestBludger)
        {
            float distanceToClosestBludger =
                (closestBludger != null)
                    ? Vector3.Distance(
                        this.gameObject.transform.position,
                        closestBludger.transform.position
                    )
                    : float.MaxValue;
            foreach (Bludger bludger in this.MyGroupAI.TheBludgers)
            {
                // Assuming that we have only two bludgers.
                if (
                    bludger.gameObject != closestBludger
                    && Vector3.Distance(
                        this.gameObject.transform.position,
                        bludger.gameObject.transform.position
                    ) < distanceToClosestBludger
                )
                {
                    closestBludger = bludger.gameObject;
                    distanceToClosestBludger = Vector3.Distance(
                        this.gameObject.transform.position,
                        bludger.gameObject.transform.position
                    );
                    break;
                }
            }

            return (closestBludger, distanceToClosestBludger);
        }
    }
}