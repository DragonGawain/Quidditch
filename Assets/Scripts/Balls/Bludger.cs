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

    // Tags corresponding to the possible targets for the bludger.
    private string[] tags = { "seeker", "keeper", "beater", "chaser" };

    // Beater currently able to hit.
    [SerializeField] private GameObject myHitter;
    public GameObject MyHitter
    { get { return myHitter; } set { myHitter = value; } }

    // Beater who previously hit.
    [SerializeField] private GameObject myPreviousHitter;
    public GameObject MyPreviousHitter { get { return myPreviousHitter; } set { myPreviousHitter = value; } }






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
        target = GetClosestTarget(tags);

        if (target != null)
        {
            Vector3 desiredVelocity = Vector3.ClampMagnitude(
            myNPCMovement.Seek(target.position, acceleration) + myRigidbody.velocity,
            maxSpeed);
            myRigidbody.velocity = desiredVelocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(string.Format("Bludger {1} collided with {0}.", collision.gameObject, this.gameObject));

        // TODO: Make sure the beater was seeking the bludger, not hit by an enemy one.
        GroupAI theBeater = collision.gameObject.GetComponent<GroupAI>();
        if (theBeater != null && collision.gameObject.CompareTag("beater"))
        {
            // TODO: set beater GroupAI variable.
            //theBeater.SetHasBall(true);
            myHitter = collision.gameObject;
            myPreviousHitter = null;
            // SetTeam(myHitter.Team);

            MyRigidbody.velocity = Vector3.zero;
            MyRigidbody.isKinematic = true;
            gameObject.transform.parent = collision.gameObject.transform;
        }
    }

    // Actions.
    public void Throw(Vector3 force)
    {
        GroupAI theBeater = myHitter.GetComponent<GroupAI>();
        //theBeater.SetHasBall(false); TO DO: add an "AbleToHit" variable to the beater group AI.
        myPreviousHitter = myHitter;
        myHitter = null;
        //SetTeam(null);

        MyRigidbody.isKinematic = false;
        gameObject.transform.parent = null;

        // Should this partly exist in the parent Ball class?
        myRigidbody.AddForce(force, ForceMode.Impulse);
    }
}
