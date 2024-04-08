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

        protected Dictionary<string, object> blackboard = new Dictionary<string, object>();
        public Dictionary<string, object> Blackboard { get { return blackboard; } }

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
            myRigidbody.velocity = Vector3.ClampMagnitude(desiredVelocity + myRigidbody.velocity, actualSpeed);
        }

        protected float bludgerTimer = 0; 
        protected bool hitByBludger = false; // don't really need this bool but it helps my sanity
        // Per role or character information.
        [SerializeField, Range(0, 15)] // Edit these ranges if needed.
        protected float myMaxSpeed = 5f;
        protected float actualSpeed = 5f;
        // I'm cheating here. Hush. 
        public float MyMaxSpeed
        {
            get { return actualSpeed; }
        }
        public void GotHitByBludger()
        {
            actualSpeed = myMaxSpeed / 2f;
            bludgerTimer = 150; // 50 FixedUpdate's per second -> 150 FUs = 3 seconds
            hitByBludger = true;
        }
        public void RecoveredFromBludgerHit()
        {
            actualSpeed = myMaxSpeed; 
            hitByBludger = false;
        }


        [SerializeField, Range(0, 5)] // At what distance does this character consider a target as having been reached?
        protected float genericHasReachedDistance = 1f;
        public float GenericHasReachedDistance
        {
            get { return genericHasReachedDistance; }
        }

        [SerializeField, Range(0, 5)] // At what distance does this character consider a waypoint as having been reached?
        protected float waypointHasReachedDistance = 2f;
        public float WaypointHasReachedDistance
        {
            get { return waypointHasReachedDistance; }
        }

        [SerializeField, Range(0, 50)] // From what distance should this character follow a target?
        protected float followHasReachedDistance = 5f;
        public float FollowHasReachedDistance
        {
            get { return followHasReachedDistance; }
        }

        [SerializeField, Range(0, 50)] // At what distance does this character consider the enemy goalpost as having been reached?
        protected float goalpostHasReachedDistance = 20f;
        public float GoalpostHasReachedDistance
        {
            get { return goalpostHasReachedDistance; }
        }

        [SerializeField, Range(1, 50)] // At what distance from a bludger should the character begin to flee it?
        protected float bludgerFleeDistance = 5f;
        public float BludgerFleeDistance
        {
            get { return bludgerFleeDistance; }
        }

        [SerializeField, Range(0, 5)] // At what distance from a bludger should the character be able to hit it?
        protected float bludgerHitDistance = 1f;
        public float BludgerHitDistance
        {
            get { return bludgerHitDistance; }
        }

        [SerializeField, Range(1, 50)] // At what distance should the character feel concerned about an enemy held quaffle?
        protected float quaffleConcernDistance = 10f;
        public float QuaffleConcernDistance
        {
            get { return quaffleConcernDistance; }
        }

        [SerializeField, Range(1, 100)] // Modulates the force of a throw on hit ball.
        protected float ballAddedForceMultiplier = 10f;
        public float BallAddedForceMultiplier
        {
            get { return ballAddedForceMultiplier; }
        }


        // Path to follow.
        [SerializeField] protected int lastWaypointIndex = -1;
        [SerializeField] protected int nextWaypointIndex = -1;
        [SerializeField] protected Transform[] waypoints; // TODO: Change the format when we get the pathfinding nodes proper.
        public void UpdateNextWaypointIndex(bool loop = false)
        {
            lastWaypointIndex = nextWaypointIndex;
            nextWaypointIndex++;

            if (loop)
            {
                nextWaypointIndex = nextWaypointIndex % waypoints.Length;
            }
        }
        public Transform ReturnLastWaypoint()
        {
            if (-1 < lastWaypointIndex && lastWaypointIndex < waypoints.Length)
            {
                return waypoints[lastWaypointIndex];
            }
            else
            {
                return null;
            }
        }
        public Transform ReturnNextWaypoint()
        {
            if (-1 < nextWaypointIndex && nextWaypointIndex < waypoints.Length)
            {
                return waypoints[nextWaypointIndex];
            }
            else
            { 
                return null;
            }
        }
        public Transform ReturnFinalWaypoint()
        {
            return waypoints[waypoints.Length - 1];
        }



        // METHODS
        protected abstract Node PlantTheTree(BehaviourTree self);

        protected void Awake()
        {
            // Collect other references.
            myNPCMovement = gameObject.GetComponent<NPCMovement>();
            myGroupAI = gameObject.GetComponent<GroupAI>();
            myRigidbody = GetComponent<Rigidbody>(); // Extra note from Craig: you don't need to say gameObject.GetComponent<>, the gameObject is implied) // Roxane: I like being explicit my dude :P
            actualSpeed = myMaxSpeed;
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
            if (hitByBludger)
            {
                bludgerTimer--;
                if (bludgerTimer <= 0)
                {
                    RecoveredFromBludgerHit();
                }
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

        public (GameObject, float) LocateClosestBludgerToTarget(GameObject target)
        {
            GameObject closestBludger = null;
            float distanceToClosestBludger = float.MaxValue;

            foreach (Bludger bludger in this.MyGroupAI.TheBludgers)
            {
                if (Vector3.Distance(target.transform.position, bludger.gameObject.transform.position) < distanceToClosestBludger)
                {
                    closestBludger = bludger.gameObject;
                    distanceToClosestBludger = Vector3.Distance(target.transform.position,bludger.gameObject.transform.position);
                }
            }

            return (closestBludger, distanceToClosestBludger);
        }
    }
}