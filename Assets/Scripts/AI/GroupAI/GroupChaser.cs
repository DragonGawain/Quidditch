using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupChaser : GroupAI
{
    // static GroupAI[] chaserFormation = new GroupAI[] { null, null, null }; // list of the chasers in order - pos 0 = leader
    // static GroupAI closestBeater = null;

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
        //     if (theQuaffle.GetTeam() == team)
        //     {
        //         if (hasBall)
        //             chaserFormation[0] = this;
        //         else if (chaserFormation[1] == null)
        //             chaserFormation[1] = this;
        //         else
        //             chaserFormation[2] = this;
        //     }
        //     else
        //         for (int i = 0; i < 3; i++)
        //             chaserFormation[i] = null;
    }

    private void FixedUpdate()
    {
        if (team == Team.AI && hasBall)
        {
            switch (AIFormation.GetFormationType())
            {
                case FormationType.NONE:
                    break;
                case FormationType.WINGMEN:
                    Debug.Log("hit 2");
                    GroupAI[] chaserFormation = AIFormation.GetChasers();
                    GroupAI[] beaterFormation = AIFormation.GetBeaters();
                    // offset back and to the left
                    chaserFormation[1].transform.position =
                        chaserFormation[0].transform.position
                        - (
                            chaserFormation[0].GetForwardRef() * 5
                            + chaserFormation[0].GetRightRef() * 3
                        );
                    // offset back and to the right
                    chaserFormation[2].transform.position =
                        chaserFormation[0].transform.position
                        - chaserFormation[0].GetForwardRef() * 5
                        + chaserFormation[0].GetRightRef() * 3;
                    // offset in front
                    beaterFormation[0].transform.position =
                        chaserFormation[0].transform.position
                        + chaserFormation[0].GetForwardRef() * 4;

                    break;
            }
        }
        else if (team == Team.PLAYER && hasBall)
        {
            switch (PlayerFormation.GetFormationType())
            {
                case FormationType.NONE:
                    break;
                case FormationType.WINGMEN:
                    Debug.Log("hit 3");
                    GroupAI[] chaserFormation = PlayerFormation.GetChasers();
                    GroupAI[] beaterFormation = PlayerFormation.GetBeaters();
                    // offset back and to the left
                    chaserFormation[1].transform.position =
                        chaserFormation[0].transform.position
                        - (
                            chaserFormation[0].GetForwardRef() * 5
                            + chaserFormation[0].GetRightRef() * 3
                        );
                    // offset back and to the right
                    chaserFormation[2].transform.position =
                        chaserFormation[0].transform.position
                        - chaserFormation[0].GetForwardRef() * 5
                        + chaserFormation[0].GetRightRef() * 3;
                    // offset in front
                    beaterFormation[0].transform.position =
                        chaserFormation[0].transform.position
                        + chaserFormation[0].GetForwardRef() * 4;

                    break;
            }
        }
    }

    protected override void OnTeamObtainedQuaffle()
    {
        Debug.Log("HIT");
        // base.OnTeamObtainedQuaffle();
        if (team == Team.AI)
        {
            AIFormation.SetChasers(this, friendlyChasers[0], friendlyChasers[1]);
            AIFormation.SetBeaters(FindClosestAlliedBeater());
            AIFormation.SetFormationType(FormationType.WINGMEN);
        }
        else
        {
            PlayerFormation.SetChasers(this, friendlyChasers[0], friendlyChasers[1]);
            PlayerFormation.SetBeaters(FindClosestAlliedBeater());
            PlayerFormation.SetFormationType(FormationType.WINGMEN);
        }
    }

    protected override void OnTeamLostQuaffle()
    {
        if (team == Team.AI)
            AIFormation.ResetAll();
        else
            PlayerFormation.ResetAll();
    }

    GroupAI FindClosestAlliedBeater()
    {
        GroupAI closestBeater = null;
        float minDist = 2147483647; // integer limit
        foreach (GroupAI beater in friendlyBeaters)
        {
            if (Vector3.Distance(transform.position, beater.transform.position) < minDist)
            {
                minDist = Vector3.Distance(transform.position, beater.transform.position);
                closestBeater = beater;
            }
        }
        return closestBeater;
    }
}
