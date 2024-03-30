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

    public Formation()
    {
        chasers = new GroupAI[] { null, null, null };
        beaters = new GroupAI[] { null, null };
        keeper = null;
        seeker = null;
        formationType = FormationType.NONE;
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
        for (int i = 0; i < chasers.Length; i++)
            chasers[i] = null;

        for (int i = 0; i < beaters.Length; i++)
            beaters[i] = null;

        keeper = null;
        seeker = null;
        formationType = FormationType.NONE;
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
}
