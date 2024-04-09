using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAI
{
    public class Bludger : Ball
    {
        // VARIABLES
        // Should speed and the like be defined here, or by the parent classe?
        [SerializeField, Range(0, 15)]
        float maxSpeed = 4f;

        [SerializeField, Range(0, 10)]
        float acceleration = 1f;

        [SerializeField]
        Transform target;
        public Transform Target
        {
            get { return target; }
        }

        // Tags corresponding to the possible targets for the bludger.
        private string[] tags = { "seeker", "beater", "chaser" }; //

        // Beater who previously hit.
        [SerializeField]
        private GameObject myPreviousHitter;
        public GameObject MyPreviousHitter
        {
            get { return myPreviousHitter; }
            set { myPreviousHitter = value; }
        }

        [SerializeField]
        bool canChase = true;

        [SerializeField]
        GameObject lastHit = null;

        // METHODS.
        // Start is called before the first frame update
        void Start() { }

        // Update is called once per frame
        void Update() { }

        protected override void OnAwake()
        {
            base.OnAwake();
        }

        void FixedUpdate()
        {
            // Get the closest seeker to flee away from them
            if (canChase)
            {
                target = GetClosestTarget(tags, lastHit);

                if (target != null)
                {
                    Vector3 desiredVelocity = Vector3.ClampMagnitude(
                        myNPCMovement.Seek(target.position, maxSpeed) + myRigidbody.velocity,
                        maxSpeed
                    );
                    myRigidbody.velocity = desiredVelocity;
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(
                string.Format(
                    "Bludger {1} collided with {0}.",
                    collision.gameObject,
                    this.gameObject
                )
            );

            if (collision.gameObject.GetComponent<PlayerController>())
            {
                if (UIManager.playerRole == PlayerRole.BEATER)
                {
                    this.Throw(
                        (
                            collision.gameObject.transform.GetChild(0).GetChild(3).position
                            - collision.gameObject.transform.GetChild(0).position
                        ).normalized * 15,
                        collision.gameObject
                    );
                }
            }
            // TODO:: add extra logic for actual player (I'm assuming that an actual player won't have have a BehaviourTree script on it?)

            // Check if the other has a Behaviour tree
            if (collision.gameObject.GetComponent<BehaviourTree>())
            {
                Debug.Log("BLUDGER HIT: " + collision.gameObject);
                collision.gameObject.GetComponent<BehaviourTree>().GotHitByBludger();

                lastHit = collision.gameObject;
            }
            // loop through all the children to see if this object is holding the quaffle
            // doing it this way cause it'll just automatically work with a player as well
            // player - bludger
            for (int i = 0; i < collision.gameObject.transform.childCount; i++)
            {
                if (collision.gameObject.transform.GetChild(i).GetComponent<Quaffle>())
                {
                    Debug.Log("BLUDGER HIT PARENT WITH QUAFFLE");
                    // shoot quaffle away from bludger hit
                    Quaffle quaffle = collision.gameObject.transform
                        .GetChild(i)
                        .GetComponent<Quaffle>();

                    quaffle.Throw(
                        (
                            (
                                collision.gameObject.transform.position - this.transform.position
                            ).normalized
                            + (
                                quaffle.transform.position - collision.gameObject.transform.position
                            ).normalized
                        ).normalized
                            * collision.gameObject
                                .GetComponent<BehaviourTree>()
                                .BallAddedForceMultiplier
                    );
                    // quaffle.Throw(
                    // (
                    //     (
                    //         collision.gameObject.transform.position - this.transform.position
                    //     ).normalized
                    //     + (
                    //         quaffle.transform.position - collision.gameObject.transform.position
                    //     ).normalized
                    // ).normalized
                    // );
                }
            }

            // TODO: handle actually hitting a player.
        }

        // Actions.
        public void Throw(Vector3 force, GameObject hitter, GameObject newTarget = null)
        {
            myPreviousHitter = hitter;
            lastHit = hitter;
            target = newTarget != null ? newTarget.transform : null;

            // Should this partly exist in the parent Ball class?
            myRigidbody.AddForce(force, ForceMode.Impulse);

            // TODO: Stop it from imediately seeking a new target.
            canChase = false;
            Invoke("CanChaseAgain", 1f);
        }

        private void CanChaseAgain()
        {
            canChase = true;
        }
    }
}
