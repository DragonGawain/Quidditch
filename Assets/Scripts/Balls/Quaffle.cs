using System.Collections;
using System.Collections.Generic;
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
            // TODO:: verification that a chaser collided with the quaffle?
            GroupAI theChaser = collision.gameObject.GetComponent<GroupAI>();
            if (theChaser != null && collision.gameObject.CompareTag("chaser"))
            {
                lastHolder = myHolder;

                if (myHolder != null && myHolder != theChaser.gameObject)
                    myHolder.GetComponent<GroupAI>().SetHasBall(false);

                myHolder = collision.gameObject;
                theChaser.SetHasBall(true);

                // SetTeam(theChaser.Team);

                MyRigidbody.velocity = Vector3.zero;
                MyRigidbody.isKinematic = true;
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

        MyRigidbody.isKinematic = false;
        gameObject.transform.parent = null;

        wasCaught = true;
        wasCaughtTimer = 3;

        // Should this partly exist in the parent Ball class?
        myRigidbody.AddForce(force, ForceMode.Impulse);
    }

    public void ResetQuaffle()
    {
        MyRigidbody.isKinematic = false;
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
    }
}
