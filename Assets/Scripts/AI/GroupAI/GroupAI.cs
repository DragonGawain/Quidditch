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

public enum team
{
    PLAYER,
    AI
}

public abstract class GroupAI : MonoBehaviour
{
    PlayerController player;
    protected PlayerRole playerRole;
    protected AIType aIType;
    protected bool hasBall = false;
    protected List<GroupAI> friendlyChasers;
    protected List<GroupAI> friendlyBeaters;
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

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerRole = player.GetPlayerRole();
        // fill the lists here, but skip over the player cause it won't have the GroupAI script

        OnAwake();
    }

    abstract protected void OnAwake();
}
