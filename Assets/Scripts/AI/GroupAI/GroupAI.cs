using System.Collections;
using System.Collections.Generic;
using CharacterAI;
using UnityEngine;

public enum AIType
{
    CHASER,
    BEATER,
    KEEPER,
    SEAKER
}

// TODO:: actually make these states make sense (each individual AI type should be represented in this enum. I just, made some stuff up for this)
public enum AIState
{
    AGGRESSIVE,
    PASSIVE,
    STEALING,
    DEFENSIVE,
    GOALIE,
    SEEKING
}

public enum Team
{
    PLAYER,
    AI,
    NONE
}

public abstract class GroupAI : MonoBehaviour
{
    PlayerController player;
    protected PlayerRole playerRole;
    public PlayerRole PlayersRole
    {
        get { return playerRole; }
    }
    protected AIType aIType;

    [SerializeField]
    protected Team team;
    public Team Team
    {
        get { return team; }
    }
    protected bool hasBall = false;
    public bool HasBall
    {
        get { return hasBall; }
        set { hasBall = value; }
    }
    protected List<GroupAI> friendlyChasers = new();
    public List<GroupAI> FriendlyChasers
    {
        get { return friendlyChasers; }
    }
    protected List<GroupAI> friendlyBeaters = new();
    protected GroupAI freindlySeeker;
    protected GroupAI freindlyKeeper;
    protected AIState state;

    protected Snitch theSnitch;
    public Snitch TheSnitch
    {
        get { return theSnitch; }
    }
    protected Quaffle theQuaffle;
    public Quaffle TheQuaffle
    {
        get { return theQuaffle; }
    }
    protected List<Bludger> theBludgers = new();
    public List<Bludger> TheBludgers
    {
        get { return theBludgers; }
    }

    protected Goalpost ourGoalpost;
    public Goalpost OurGoalpost
    {
        get { return ourGoalpost; }
    }
    protected Goalpost enemyGoalpost;
    public Goalpost EnemyGoalpost
    {
        get { return enemyGoalpost; }
    }

    protected Rigidbody rb;

    protected Transform upRef;
    protected Transform rightRef;
    protected Transform forwardRef;

    static protected Formation AIFormation = new();
    static protected Formation PlayerFormation = new();
    protected Formation MyFormation = new();

    protected Vector3 prevPos; // used to make AI face the direction that it's moving
    protected Vector3 prevPos2;
    protected Vector3 prevPos3;

    protected Vector3 formationPosition;

    protected bool isInFormation = false;

    public void SetIsinFormation(bool val)
    {
        isInFormation = val;
    }

    public bool GetIsInFormation()
    {
        return isInFormation;
    }

    public AIState GetState()
    {
        return state;
    }

    public AIType GetAIType()
    {
        return aIType;
    }

    public Team GetTeam()
    {
        return team;
    }

    public Vector3 GetUpRef()
    {
        return upRef.position - transform.position;
    }

    public Vector3 GetRightRef()
    {
        return rightRef.position - transform.position;
    }

    public Vector3 GetForwardRef()
    {
        return forwardRef.position - transform.position;
    }

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

    private void Awake()
    {
        // rigidbody
        rb = GetComponent<Rigidbody>();
        upRef = transform.GetChild(0);
        rightRef = transform.GetChild(1);
        forwardRef = transform.GetChild(2);

        // Find the player.
        // Roxane: add to add a check to keep it from glitching in my test scene.
        GameObject potentialPlayer = GameObject.FindGameObjectWithTag("Player");
        if (potentialPlayer != null)
        {
            player = potentialPlayer.GetComponent<PlayerController>();
            playerRole = player.GetPlayerRole();
        }

        // Find the balls.
        GameObject potentialSnitch = GameObject.FindGameObjectWithTag("snitch");
        if (potentialSnitch != null)
        {
            theSnitch = potentialSnitch.GetComponent<Snitch>();
        }
        GameObject potentialQuaffle = GameObject.FindGameObjectWithTag("quaffle");
        if (potentialQuaffle != null)
        {
            theQuaffle = potentialQuaffle.GetComponent<Quaffle>();
        }
        GameObject[] potentialBludgers = GameObject.FindGameObjectsWithTag("bludger");
        if (potentialBludgers != null)
        {
            foreach (GameObject potentialBludger in potentialBludgers)
            {
                Bludger bludger = potentialBludger.GetComponent<Bludger>();
                if (bludger != null)
                {
                    theBludgers.Add(bludger);
                }
            }
        }

        // Find the goalposts.
        // TODO:: I thought wach team was supposed to have 3 goal posts? Might need to tweak this a bit..
        GameObject[] potentialGoalpostGOs = GameObject.FindGameObjectsWithTag("goalpost");
        if (potentialGoalpostGOs != null && potentialGoalpostGOs.Length > 0)
        {
            foreach (GameObject potentialGoalpostGO in potentialGoalpostGOs)
            {
                Goalpost potentialGoalpost = potentialGoalpostGO.GetComponent<Goalpost>();

                if (potentialGoalpost.OwningTeam == team)
                {
                    ourGoalpost = potentialGoalpost;
                }
                else
                {
                    enemyGoalpost = potentialGoalpost;
                }
            }
        }

        // fill the lists here, but skip over the player cause it won't have the GroupAI script
        GameObject[] chasers = GameObject.FindGameObjectsWithTag("chaser");
        GameObject[] beaters = GameObject.FindGameObjectsWithTag("beater");
        GameObject[] keepers = GameObject.FindGameObjectsWithTag("keeper");
        GameObject[] seekers = GameObject.FindGameObjectsWithTag("seeker");

        // Note that whenever I do stuff with GroupAI, it also applies to the children. Hence why the abstraction is super handy here.
        // fill in friendlyChasers
        foreach (GameObject chaser in chasers)
        {
            // if we found the player (does not have GroupAI script), or it is this object, continue
            if (chaser.GetComponent<GroupAI>() == null || chaser == this.gameObject)
                continue;
            // Get the GroupAI script
            GroupAI ai = chaser.GetComponent<GroupAI>();
            // If the target is on the same team, add it to the list
            if (ai.GetTeam() == this.team)
                friendlyChasers.Add(ai);
        }

        // fill in friendlyBeaters
        foreach (GameObject beater in beaters)
        {
            // if we found the player (does not have GroupAI script), or it is this object, continue
            if (beater.GetComponent<GroupAI>() == null || beater == this.gameObject)
                continue;
            // Get the GroupAI script
            GroupAI ai = beater.GetComponent<GroupAI>();
            // If the target is on the same team, add it to the list
            if (ai.GetTeam() == this.team)
                friendlyBeaters.Add(ai);
        }

        // fill in friendlyKeeper
        foreach (GameObject keeper in keepers)
        {
            // if we found the player (does not have GroupAI script), or it is this object, continue
            if (keeper.GetComponent<GroupAI>() == null || keeper == this.gameObject)
                continue;
            // Get the GroupAI script
            GroupAI ai = keeper.GetComponent<GroupAI>();
            // If the target is on the same team, assign it (there should only be 1 keeper)
            if (ai.GetTeam() == this.team)
                freindlyKeeper = ai;
        }

        // fill in friendlySeeker
        foreach (GameObject seeker in seekers)
        {
            // if we found the player (does not have GroupAI script), or it is this object, continue
            if (seeker.GetComponent<GroupAI>() == null || seeker == this.gameObject)
                continue;
            // Get the GroupAI script
            GroupAI ai = seeker.GetComponent<GroupAI>();
            // If the target is on the same team, assign it (there should only be 1 seeker)
            if (ai.GetTeam() == this.team)
                freindlySeeker = ai;
        }

        prevPos = transform.position;
        prevPos2 = transform.position;
        prevPos3 = transform.position;
        OnAwake();
    }

    abstract protected void OnAwake();

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

    private void LateUpdate()
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
