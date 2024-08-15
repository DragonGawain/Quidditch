using System.Collections;
using System.Collections.Generic;
using CharacterAI;
using UnityEngine;

public class Quaffle : Ball
{
    // VARIABLES
    // Should speed and the like be defined here, or by the parent classe?
    [SerializeField]
    private GameObject holder = null;
    public GameObject Holder
    {
        get { return holder; }
        set { holder = value; }
    }
    public GameObject getOwner()
    {
        return holder;
    }

    // Important for tracking who is the chaser (and what is the team) that scored
    [SerializeField]
    private GameObject lastOwner;
    public GameObject LastOwner
    {
        get { return lastOwner; }
        set { lastOwner = value; }
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
                lastOwner = holder;

                if (holder != null && holder != theCatcher.gameObject)
                {
                    holder.GetComponent<GroupAI>().SetHasBall(false);
                    holder.GetComponent<BehaviourTree>().QuaffleBoostEnd();
                }

                holder = collision.gameObject;
                theCatcher.SetHasBall(true);

                // check if the myHolder has a Behaviour tree -> if it does, give them a speed boost
                if (holder.GetComponent<BehaviourTree>())
                    holder.GetComponent<BehaviourTree>().CollectedQuaffle();

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
            if (theChaser != null && collision.gameObject.CompareTag("chaser") && holder == null)
            {
                lastOwner = holder;

                if (holder != null && holder != theChaser.gameObject)
                {
                    holder.GetComponent<GroupAI>().SetHasBall(false);
                    holder.GetComponent<BehaviourTree>().QuaffleBoostEnd();
                }

                holder = collision.gameObject;
                theChaser.SetHasBall(true);

                // check if the myHolder has a Behaviour tree -> if it does, give them a speed boost
                if (holder.GetComponent<BehaviourTree>())
                    holder.GetComponent<BehaviourTree>().CollectedQuaffle();

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
        GroupAI theChaser = holder.GetComponent<GroupAI>();
        theChaser.SetHasBall(false);

        lastOwner = holder;
        holder = null;

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

        if (holder != null)
        {
            if (holder.GetComponent<GroupAI>())
                holder.GetComponent<GroupAI>().SetHasBall(false);
        }

        holder = null;
        lastOwner = null;

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
        if (holder == null)
        {
            MyRigidbody.velocity = MyRigidbody.velocity - (MyRigidbody.velocity.normalized * 0.05f);
        }
    }
}
