using System.Collections;
using System.Collections.Generic;
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
    public PlayerRole PlayersRole { get { return playerRole; } }
    protected AIType aIType;

    [SerializeField]
    protected Team team;
    public Team Team { get { return team; } }
    protected bool hasBall = false;
    public bool HasBall { get { return hasBall; } }
    protected List<GroupAI> friendlyChasers = new();
    public List<GroupAI> FriendlyChasers { get { return friendlyChasers; } }
    protected List<GroupAI> friendlyBeaters = new();
    protected GroupAI freindlySeeker;
    protected GroupAI freindlyKeeper;
    protected AIState state;

    protected Snitch theSnitch;
    public Snitch TheSnitch { get { return theSnitch; } }
    protected Quaffle theQuaffle;
    public Quaffle TheQuaffle { get { return theQuaffle; } }
    protected List<Bludger> theBludgers = new();
    public List<Bludger> TheBludgers { get { return theBludgers; } }

    protected Goalpost ourGoalpost;
    public Goalpost OurGoalpost { get { return ourGoalpost; } }
    protected Goalpost enemyGoalpost;
    public Goalpost EnemyGoalpost { get { return enemyGoalpost; } }

    //
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


    private void Awake()
    {
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
        GameObject[] potentialGoalpostGOs = GameObject.FindGameObjectsWithTag("goalpost");
        if (potentialGoalpostGOs != null && potentialGoalpostGOs.Length > 0)
        {
            // We assume that there are at most two goalposts.
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

        OnAwake();
    }

    abstract protected void OnAwake();
}
