using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quaffle : Ball
{
    // VARIABLES
    // Should speed and the like be defined here, or by the parent classe?
    [SerializeField] private GameObject myHolder;
    public GameObject MyHolder { get { return myHolder; } set { myHolder = value; } }

    private Team teamWithQuaffle = Team.NONE;
    public void SetTeam(Team team)
    {
        teamWithQuaffle = team;
    }
    public Team GetTeam()
    {
        return teamWithQuaffle;
    }



    // METHODS
    // Built in.
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(string.Format("The quaffle collided with {0}.", collision.gameObject));

        GroupAI theChaser = collision.gameObject.GetComponent<GroupAI>();
        if (theChaser != null && collision.gameObject.CompareTag("chaser"))
        {
            theChaser.HasBall = true;
            myHolder = collision.gameObject;
            SetTeam(theChaser.Team);

            MyRigidbody.isKinematic = true;
            gameObject.transform.parent = collision.gameObject.transform;
        }
    }

    // Actions.
    public void Throw(Vector3 force) 
    {
        Debug.Log("In THROW");

        GroupAI theChaser = myHolder.GetComponent<GroupAI>();
        theChaser.HasBall = false;
        myHolder = null;
        //SetTeam(null);

        MyRigidbody.isKinematic = false;
        gameObject.transform.parent = null;

        // Should this partly exist in the parent Ball class?
        myRigidbody.AddForce(force, ForceMode.Impulse);
    }
}
