using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Transform Target { get { return target; } }

    // Tags corresponding to the possible targets for the bludger.
    private string[] tags = { "seeker", "keeper", "beater", "chaser" };

    // Beater who previously hit.
    [SerializeField] private GameObject myPreviousHitter;
    public GameObject MyPreviousHitter { get { return myPreviousHitter; } set { myPreviousHitter = value; } }

    [SerializeField]
    bool canChase = true;




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
            target = GetClosestTarget(tags);

            if (target != null)
            {
                Vector3 desiredVelocity = Vector3.ClampMagnitude(myNPCMovement.Seek(target.position, maxSpeed) + myRigidbody.velocity, maxSpeed);
                myRigidbody.velocity = desiredVelocity;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(string.Format("Bludger {1} collided with {0}.", collision.gameObject, this.gameObject));

        // TODO: handle actually hitting a player.
    }

    // Actions.
    public void Throw(Vector3 force, GameObject hitter)
    {
        myPreviousHitter = hitter;
        target = null;

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
