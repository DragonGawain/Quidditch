using System.Collections;
using System.Collections.Generic;
using CharacterAI;
using UnityEngine;

public class Quaffle : Ball
{
    // VARIABLES
    // Should speed and the like be defined here, or by the parent classe?
    [SerializeField]
    private GameObject myHolder = null;
    public GameObject MyHolder
    {
        get { return myHolder; }
        set { myHolder = value; }
    }

    // Important for tracking who is the chaser (and what is the team) that scored
    [SerializeField]
    private GameObject lastHolder;
    public GameObject LastHolder
    {
        get { return lastHolder; }
        set { lastHolder = value; }
    }

    int wasCaughtTimer = 0;

    [SerializeField]
    bool wasCaught = false;

    // private Team teamWithQuaffle = Team.NONE;
    // public void SetTeam(Team team)
    // {
    //     teamWithQuaffle = team;
    // }
    // public Team GetTeam()
    // {
    //     return teamWithQuaffle;
    // }



    // METHODS
    // Built in.
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(string.Format("The quaffle collided with {0}.", collision.gameObject));
        if (!wasCaught)
        {
            GroupAI theCatcher = collision.gameObject.GetComponent<GroupAI>();
            if (theCatcher != null && collision.gameObject.CompareTag("chaser"))
            {
                lastHolder = myHolder;

                if (myHolder != null && myHolder != theCatcher.gameObject)
                {
                    myHolder.GetComponent<GroupAI>().SetHasBall(false);
                    myHolder.GetComponent<BehaviourTree>().QuaffleBoostEnd();
                }

                myHolder = collision.gameObject;
                theCatcher.SetHasBall(true);

                // check if the myHolder has a Behaviour tree -> if it does, give them a speed boost
                if (myHolder.GetComponent<BehaviourTree>())
                    myHolder.GetComponent<BehaviourTree>().CollectedQuaffle();

                // SetTeam(theChaser.Team);

                MyRigidbody.velocity = Vector3.zero;
                // MyRigidbody.isKinematic = true;
                myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                this.gameObject.transform.parent = collision.gameObject.transform;
            }

            if (theCatcher != null && collision.gameObject.CompareTag("keeper"))
            {
                ResetQuaffle();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!wasCaught)
        {
            Debug.Log(
                string.Format(
                    "The quaffle is colliding  with {0} continuously.",
                    collision.gameObject
                )
            );
            GroupAI theChaser = collision.gameObject.GetComponent<GroupAI>();
            if (theChaser != null && collision.gameObject.CompareTag("chaser") && myHolder == null)
            {
                lastHolder = myHolder;

                if (myHolder != null && myHolder != theChaser.gameObject)
                {
                    myHolder.GetComponent<GroupAI>().SetHasBall(false);
                    myHolder.GetComponent<BehaviourTree>().QuaffleBoostEnd();
                }

                myHolder = collision.gameObject;
                theChaser.SetHasBall(true);

                // check if the myHolder has a Behaviour tree -> if it does, give them a speed boost
                if (myHolder.GetComponent<BehaviourTree>())
                    myHolder.GetComponent<BehaviourTree>().CollectedQuaffle();

                // SetTeam(theChaser.Team);

                MyRigidbody.velocity = Vector3.zero;
                // MyRigidbody.isKinematic = true;
                myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                this.gameObject.transform.parent = collision.gameObject.transform;
            }
        }
    }

    // Actions.
    public void Throw(Vector3 force)
    {
        Debug.Log("thrown");
        GroupAI theChaser = myHolder.GetComponent<GroupAI>();
        theChaser.SetHasBall(false);

        lastHolder = myHolder;
        myHolder = null;

        // MyRigidbody.isKinematic = false;
        myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        gameObject.transform.parent = null;

        wasCaught = true;
        wasCaughtTimer = 3;

        // Should this partly exist in the parent Ball class?
        myRigidbody.AddForce(force, ForceMode.Impulse);
    }

    public void ResetQuaffle()
    {
        // MyRigidbody.isKinematic = false;
        myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        gameObject.transform.parent = null;

        transform.position = Vector3.zero;
        MyRigidbody.velocity = Vector3.zero;

        myHolder = null;
        lastHolder = null;

        wasCaught = false;
    }

    private void FixedUpdate()
    {
        if (wasCaught)
        {
            if (wasCaughtTimer <= 0)
                wasCaught = false;
            wasCaughtTimer--;
        }
        if (myHolder == null)
        {
            MyRigidbody.velocity = MyRigidbody.velocity - (MyRigidbody.velocity.normalized * 0.05f);
        }
    }
}
