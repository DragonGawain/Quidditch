using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupChaser : GroupAI
{
    // static GroupAI[] chaserFormation = new GroupAI[] { null, null, null }; // list of the chasers in order - pos 0 = leader
    // static GroupAI closestBeater = null;

    protected override void OnAwake()
    {
        // determine which team this AI is on
        if (team == Team.AI && hasBall)
            MyFormation = AIFormation;
        else
            MyFormation = PlayerFormation;

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

    protected override void FixedUpdate()
    {
        if (hasBall)
        {
            if (team == Team.AI)
                MyFormation = AIFormation;
            else
                MyFormation = PlayerFormation;
            GroupAI[] chaserFormation = MyFormation.GetChasers();
            GroupAI[] beaterFormation = MyFormation.GetBeaters();
            switch (MyFormation.GetFormationType())
            {
                case FormationType.NONE:
                    break;
                case FormationType.WINGMEN:
                    // Debug.Log(
                    //     "hit W chase: "
                    //         + MyFormation.GetChasers()
                    //         + ", hit W beat: "
                    //         + MyFormation.GetBeaters()
                    // );
                    // offset back and to the left
                    // The AI at the index chaserFormation[1] should seek towards this point
                    chaserFormation[1].SetFormationPosition(
                        chaserFormation[0].transform.position
                            - (
                                chaserFormation[0].GetForwardRef() * 5
                                + chaserFormation[0].GetRightRef() * 3
                            )
                    );
                    // offset back and to the right
                    chaserFormation[2].SetFormationPosition(
                        chaserFormation[0].transform.position
                            - chaserFormation[0].GetForwardRef() * 5
                            + chaserFormation[0].GetRightRef() * 3
                    );
                    // offset in front
                    beaterFormation[0].SetFormationPosition(
                        chaserFormation[0].transform.position
                            + chaserFormation[0].GetForwardRef() * 4
                    );
                    break;
                case FormationType.BEATFLANK:
                    // Debug.Log(
                    //     "hit B chase: "
                    //         + MyFormation.GetChasers()
                    //         + ", hit B beat: "
                    //         + MyFormation.GetBeaters()
                    // );
                    beaterFormation[0].SetFormationPosition(
                        chaserFormation[0].transform.position
                            + chaserFormation[0].GetRightRef() * 2.5f
                    );
                    beaterFormation[0].SetFormationPosition(
                        chaserFormation[0].transform.position
                            - chaserFormation[0].GetRightRef() * 2.5f
                    );
                    break;
                case FormationType.ALIGN:
                    // Debug.Log(
                    //     "hit A chase: "
                    //         + MyFormation.GetChasers()
                    //         + ", hit A beat: "
                    //         + MyFormation.GetBeaters()
                    // );
                    // offset right
                    chaserFormation[1].SetFormationPosition(
                        chaserFormation[0].transform.position
                            + chaserFormation[0].GetRightRef() * 3.25f
                    );
                    // offset the left
                    chaserFormation[2].SetFormationPosition(
                        chaserFormation[0].transform.position
                            - chaserFormation[0].GetRightRef() * 3.25f
                    );
                    break;
            }
        }
        if (MyFormation.GetFormationType() != FormationType.NONE)
        {
            if (team == Team.AI)
                AIFormation = MyFormation;
            else
                PlayerFormation = MyFormation;
        }

        base.FixedUpdate();
    }

    protected override void OnTeamObtainedQuaffle()
    {
        Debug.Log("HIT");
        // foreach (var item in friendlyChasers)
        // {
        //     Debug.Log(item.name);
        // }
        // base.OnTeamObtainedQuaffle();
        int selector = Mathf.FloorToInt(Random.Range(0, 2.99f));
        switch (selector)
        {
            case 0:
                MyFormation.SetFormationFlag(FormationType.WINGMEN, true);
                MyFormation.SetChasers(this, friendlyChasers[0], friendlyChasers[1]);
                MyFormation.SetBeaters(FindClosestAlliedBeater());
                MyFormation.SetKeeper();
                MyFormation.SetSeeker();
                // GroupAI[] beaterFormation1 = MyFormation.GetBeaters();
                // foreach (GroupAI beater in beaterFormation1)
                // {
                //     if (beater != null)
                //         beater.SetStance(AIStance.DEFENSIVE);
                // }
                // Set beater to aggressive/defensive
                break;
            case 1:
                MyFormation.SetFormationFlag(FormationType.BEATFLANK, true);
                MyFormation.SetChasers(this);
                MyFormation.SetBeaters(friendlyBeaters[0], friendlyBeaters[1]);
                MyFormation.SetKeeper();
                MyFormation.SetSeeker();
                // GroupAI[] beaterFormation2 = MyFormation.GetBeaters();
                // foreach (GroupAI beater in beaterFormation2)
                // {
                //     if (beater != null)
                //         beater.SetStance(AIStance.DEFENSIVE);
                // }
                // Set beaters to aggressive/defensive
                break;
            case 2:
                MyFormation.SetFormationFlag(FormationType.ALIGN, true);
                MyFormation.SetChasers(this, friendlyChasers[0], friendlyChasers[1]);
                MyFormation.SetBeaters();
                MyFormation.SetKeeper();
                MyFormation.SetSeeker();
                break;
            default:
                MyFormation.SetFormationFlag(FormationType.WINGMEN, true);
                MyFormation.SetChasers(this, friendlyChasers[0], friendlyChasers[1]);
                MyFormation.SetBeaters(FindClosestAlliedBeater());
                MyFormation.SetKeeper();
                MyFormation.SetSeeker();
                break;
        }

        if (team == Team.AI)
            AIFormation = MyFormation;
        else
            PlayerFormation = MyFormation;
        // MyFormation.SetFormationType(FormationType.WINGMEN);
    }

    protected override void OnTeamLostQuaffle()
    {
        // MyFormation.SetFormationFlag(FormationType.WINGMEN, false);
        // MyFormation.SetFormationFlag(FormationType.BEATFLANK, false);
        // MyFormation.SetFormationFlag(FormationType.ALIGN, false);
        MyFormation.ResetAll();
        if (team == Team.AI)
            AIFormation = MyFormation;
        else
            PlayerFormation = MyFormation;
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
