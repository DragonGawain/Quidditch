using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FormationType
{
    NONE,
    WINGMEN
}

public class Formation
{
    // I'm using full getters/setters here because I want to set the values in a particular way - namely, the unused elements of the arrays wil automatically be set to null.
    // (also I just prefer the way getters.setters look - the visual appeal of the call is worth some value to me)
    GroupAI[] chasers;
    GroupAI[] beaters;
    GroupAI keeper;
    GroupAI seeker;
    FormationType formationType;
    Dictionary<FormationType, bool> formationFlags = new();

    public Formation()
    {
        chasers = new GroupAI[] { null, null, null };
        beaters = new GroupAI[] { null, null };
        keeper = null;
        seeker = null;
        formationType = FormationType.NONE;
        // Add a flag for every type of formation
        // The order of the formations is order of precedence -> first true flag in the list will be the formation
        formationFlags.Add(FormationType.WINGMEN, false);
    }

    public void SetChasers(GroupAI c0 = null, GroupAI c1 = null, GroupAI c2 = null)
    {
        chasers[0] = c0;
        chasers[1] = c1;
        chasers[2] = c2;
    }

    public void SetBeaters(GroupAI b0 = null, GroupAI b1 = null)
    {
        beaters[0] = b0;
        beaters[1] = b1;
    }

    public void SetKeeper(GroupAI k = null)
    {
        keeper = k;
    }

    public void SetSeeker(GroupAI s = null)
    {
        seeker = s;
    }

    public void ResetAll()
    {
        formationType = FormationType.NONE;
        for (int i = 0; i < chasers.Length; i++)
        {
            // if (chasers[i] != null)
            //     chasers[i].SetFormationPosition(null);
            chasers[i] = null;
        }

        for (int i = 0; i < beaters.Length; i++)
        {
            // if (beaters[i] != null)
            //     beaters[i].SetFormationPosition(null);
            beaters[i] = null;
        }

        // if (keeper != null)
        //     keeper.SetFormationPosition(null);
        keeper = null;
        // if (seeker != null)
        //     seeker.SetFormationPosition(null);
        seeker = null;
    }

    public GroupAI[] GetChasers()
    {
        return chasers;
    }

    public GroupAI[] GetBeaters()
    {
        return beaters;
    }

    public GroupAI GetKeeper()
    {
        return keeper;
    }

    public GroupAI GetSeeker()
    {
        return seeker;
    }

    public void SetFormationType(FormationType ft)
    {
        formationType = ft;
    }

    public FormationType GetFormationType()
    {
        return formationType;
    }

    public void SetFormationFlag(FormationType key, bool val)
    {
        formationFlags[key] = val;
        chooseFormation();
    }

    public bool GetFormationFlag(FormationType key)
    {
        return formationFlags[key];
    }

    private void chooseFormation()
    {
        // Check the flags one by one, and set the flag to the first one that is true.
        foreach (KeyValuePair<FormationType, bool> flag in formationFlags)
        {
            if (flag.Value == true)
            {
                formationType = flag.Key;
                return;
            }
        }
        formationType = FormationType.NONE;
    }
}

// experimental, not sure if I'll do this
// public class FormationPosition
// {
//     public GroupAI groupAI;
//     public Vector3 position;
// }
