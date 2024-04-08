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
    private GameObject lastHolder;
    public GameObject LastHolder
    {
        get { return lastHolder; }
        set { lastHolder = value; }
    }

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

        // TODO:: verification that a chaser collided with the quaffle?
        GroupAI theChaser = collision.gameObject.GetComponent<GroupAI>();
        if (theChaser != null && collision.gameObject.CompareTag("chaser"))
        {
            if (myHolder != null && myHolder != theChaser.gameObject)
                myHolder.GetComponent<GroupAI>().SetHasBall(false);

            theChaser.SetHasBall(true);
            myHolder = collision.gameObject;
            lastHolder = myHolder;
            // SetTeam(theChaser.Team);

            MyRigidbody.velocity = Vector3.zero;
            MyRigidbody.isKinematic = true;
            gameObject.transform.parent = collision.gameObject.transform;
        }
    }

    // Actions.
    public void Throw(Vector3 force)
    {
        GroupAI theChaser = myHolder.GetComponent<GroupAI>();
        theChaser.SetHasBall(false);
        myHolder = null;
        //SetTeam(null);

        MyRigidbody.isKinematic = false;
        gameObject.transform.parent = null;

        // Should this partly exist in the parent Ball class?
        myRigidbody.AddForce(force, ForceMode.Impulse);
    }
}
