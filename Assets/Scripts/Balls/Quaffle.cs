using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quaffle : Ball
{
    // VARIABLES
    // Should speed and the like be defined here, or by the parent classe?
    Team teamWithQuaffle = Team.NONE;
    public void SetTeam(Team team)
    {
        teamWithQuaffle = team;
    }
    public Team GetTeam()
    {
        return teamWithQuaffle;
    }



    // METHODS.
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    
}
