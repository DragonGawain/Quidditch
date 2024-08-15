using System.Collections;
using System.Collections.Generic;
using CharacterAI;
using UnityEngine;

using groupAI;

namespace groupAI
{ 
    public enum stance
    {
        AGGRESSIVE,
        DEFENSIVE,
    }
    public enum team// TODO:: actually make these states make sense (each individual AI type should be represented in this enum. I just, made some stuff up for this)

    {
        PLAYER,
        AI,
        NONE
    }
    public enum FormationType
    {
        NONE,
        WINGMEN,
        BEATFLANK,
        ALIGN
    }
}


public class GroupAI : MonoBehaviour
{
    [SerializeField]
    protected groupAI.stance stance;
    public groupAI.stance Stance
    {
        get { return stance; }
        set { stance = value; }
    }

    static protected Formation AIFormation;
    static protected Formation PlayerFormation;
    protected Formation MyFormation;

    protected Vector3 prevPos; // used to make AI face the direction that it's moving
    protected Vector3 prevPos2;
    protected Vector3 prevPos3;

    protected Vector3 formationPosition;

    protected bool isInFormation = false;
    public bool InFormation
    {
        get { return isInFormation; }
        set { isInFormation = value; }
    }

    protected TeamManager team;
    public TeamManager Team { get { return team; } }

    protected GameManager game;

    protected bool hasBall = false;
    public bool HasBall
    {
        get { return hasBall; }
        set { hasBall = value; }
    }


    public GameObject[] chasers
    {
        get { return team.Chasers; }
    }
    public GameObject[] beaters
    {
        get { return team.Beaters; }
    }

    public GameObject seeker
    {
        get { return team.Seeker; }
    }

    public GameObject keeper
    {
        get { return team.Keeper; }
    }

    public TeamManager enemyTeam
    {
        get { return team.EnemyTeam; } 
    }

    protected bool hasPlayer
    {
        get { return team.HasPlayer; }
    }
    public GameObject[] goalposts
    {
        get { return team.Goalposts; }
    }

    public GameObject snitch
    {
        get { return game.Snitch; }
    }
    public  GameObject quaffle
    {
        get { return game.Quaffle; }
    }
    public GameObject[] bludgers
    {
        get { return game.Bludgers; }
    }


    public void init()
    {
        team = GetComponentInParent<TeamManager>();
        if(team == null)
        {
            Debug.Log("Group AI does not have TeamManager");
        }
    }

    public void OnAwake()
    {
        if (hasPlayer && hasBall)
            MyFormation = AIFormation;
        else
            MyFormation = PlayerFormation;
    }

    public bool isAggressive()
    {
        return (stance == groupAI.stance.AGGRESSIVE);

    }

    public bool isDefensive()
    {
        return (stance == groupAI.stance.DEFENSIVE);
    }
  

    //public Vector3 GetUpRef()
    //{
    //    return upRef.position - transform.position;
    //}

    //public Vector3 GetRightRef()
    //{
    //    return rightRef.position - transform.position;
    //}

    //public Vector3 GetForwardRef()
    //{
    //    return forwardRef.position - transform.position;
    //}

    // Yes I know that I'm breaking standard setter practice by adding additional functionality to the setter, but frankly this is just the easiest way to detect when a team/person has picked up the quaffle
    public void SetHasBall(bool state)
    {
        hasBall = state;
        // MyFormation.SetFormationFlag(FormationType.WINGMEN, true);
        // MyFormation.SetFormationFlag(FormationType.WINGMEN, false);
        if (state)
            OnTeamObtainedQuaffle();
        else
            OnTeamLostQuaffle();
    }

    public void SetFormationPosition(Vector3 fPos)
    {
        formationPosition = fPos;
    }

    public Vector3 GetFormationPosition()
    {
        return formationPosition;
    }

    protected virtual void OnTeamObtainedQuaffle()
    {
        return;
    }

    protected virtual void OnTeamLostQuaffle()
    {
        return;
    }

    public Formation GetMyFormation()
    {
        return MyFormation;
    }

    protected virtual void FixedUpdate()
    {
        // only do rotaion stuff if it's not the player
        // if (
        //     !gameObject.name.Equals("PlayerChaser")
        //     && !gameObject.name.Equals("PlayerBeater")
        //     && !gameObject.name.Equals("PlayerKeeper")
        //     && !gameObject.name.Equals("PlayerSeeker")
        // )
        if (!gameObject.name.Equals("Player"))
        {
            // AI will face away from the point they were at last update tick (i.e. they will look in the direction they are moving)
            if (prevPos2 == prevPos3)
                transform.rotation = Quaternion.LookRotation(transform.position - prevPos);
            else
                transform.rotation = this.GetComponent<BehaviourTree>()
                    .MyNPCMovement.KinematicFaceAway(prevPos3);
            prevPos3 = prevPos2;
            prevPos2 = prevPos;
            prevPos = transform.position;
        }
    }

}
