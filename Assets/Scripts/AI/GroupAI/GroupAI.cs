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
    AI
}

public abstract class GroupAI : MonoBehaviour
{
    PlayerController player;
    protected PlayerRole playerRole;
    protected AIType aIType;

    [SerializeField]
    protected Team team;
    protected bool hasBall = false;
    protected List<GroupAI> friendlyChasers = new();
    protected List<GroupAI> friendlyBeaters = new();
    protected GroupAI freindlySeeker;
    protected GroupAI freindlyKeeper;
    protected AIState state;

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