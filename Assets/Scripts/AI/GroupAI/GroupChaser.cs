using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupChaser : GroupAI
{
    GroupAI[] chaserFormation = new GroupAI[] { null, null, null }; // list of the chasers in order - pos 0 = leader

    protected override void OnAwake()
    {
        string output = "CHASER " + this.gameObject.name + ", - chasers: ";
        foreach (GroupAI chaser in friendlyChasers)
        {
            output += chaser.gameObject.name + ", ";
        }
        output += "beaters: ";
        foreach (GroupAI beater in friendlyBeaters)
        {
            output += beater.gameObject.name + ", ";
        }
        if (freindlySeeker != null)
            output += "seeker: " + freindlySeeker.gameObject.name;
        if (freindlyKeeper != null)
            output += ", keeper: " + freindlyKeeper.gameObject.name;
        Debug.Log(output);
    }

    // Update is called once per frame
    void Update()
    {
        // check if the team has the ball
        if (theQuaffle.GetTeam() == team)
        {
            if (hasBall)
                chaserFormation[0] = this;
            else if (chaserFormation[1] == null)
                chaserFormation[1] = this;
            else
                chaserFormation[2] = this;
        }
        else
            for (int i = 0; i < 3; i++)
                chaserFormation[i] = null;
    }
}
