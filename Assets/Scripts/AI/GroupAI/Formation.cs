using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using groupAI;
using Unity.VisualScripting;


namespace formation
{
    public enum Type
    {
        NONE,
        WINGMEN,
        BEATFLANK,
        ALIGN
    }
}
public class Formation
{
    // I'm using full getters/setters here because I want to set the values in a particular way - namely, the unused elements of the arrays wil automatically be set to null.
    // (also I just prefer the way getters.setters look - the visual appeal of the call is worth some value to me)
   
    formation.Type type;
    public formation.Type Ftype
    {
        get { return type; }
        set { type = value; }
    }
    new List<bool> formationFlags; 
    //Dictionary<formation.Type, bool> formationFlags = new();

    private List<Player> chasers= new List<Player>();
    private List<Player> beaters = new List<Player>();
    Player keeper;
    Player seeker;

    public Formation( GameObject[] chasers, GameObject[] beaters, GameObject keeper, GameObject Seeker)
    {
        type = formation.Type.NONE;
        int num_types = System.Enum.GetNames(typeof(formation.Type)).Length;
        formationFlags = new List<bool>(new bool[num_types]);//default initialisation value for bools is false
        // Add a flag for every type of formation
        // The order of the formations is order of precedence -> first true flag in the list will be the formation
        

        foreach (GameObject chaser in chasers)
        {
            this.chasers.Add(chaser.GetComponent<Player>());
        }
        foreach (GameObject beater in beaters)
        {
            this.chasers.Add(beater.GetComponent<Player>());
        }

        this.keeper = keeper.GetComponent<Player>();
        this.seeker = seeker.GetComponent<Player>();


    }

    public void ResetAll()
    {
        // formationType = FormationType.NONE;
        for (int i = 0; i < chasers.Count; i++)
        {
            if (chasers[i] != null)
                chasers[i].InFormation =false;
            chasers[i] = null;
        }

        for (int i = 0; i < beaters.Count; i++)
        {
            if (beaters[i] != null)
                beaters[i].InFormation = false;
            beaters[i] = null;
        }

        if (keeper != null)
            keeper.InFormation = false;
        keeper = null;
        if (seeker != null)
            seeker.InFormation = false;
        seeker = null;


        int num_types = System.Enum.GetNames(typeof(formation.Type)).Length;
        formationFlags.Clear();
        formationFlags = new List<bool>(new bool[num_types]);//default initialisation value for bools is false


        // foreach (KeyValuePair<FormationType, bool> flag in formationFlags)
        // {
        //     formationFlags[flag.Key] = false;
        // }
    }



    public void SetFormationFlag(formation.Type flag, bool val)
    {
        formationFlags[(int)flag] = val;
        Debug.Log("SETTING FORMATION TO: " + nameof(flag));
        chooseFormation();
    }

    public bool GetFormationFlag(FormationType key)
    {
        return formationFlags[(int)key];
    }

    private void chooseFormation()
    {
        // Check the flags one by one, and set the flag to the first one that is true.
        bool flagset = false;
        int flagtype = 0;

        foreach (bool flag in formationFlags)
        {
            if (flag == true)
            {
                flagset = true;
                Debug.Log("FORMATION SET TO: " + nameof(flag));
                type = (formation.Type) flagtype;
                break;
            }
            flagtype++;
        }
        if (!flagset)
        {
            Debug.Log("NO FORMATION SET");
            type = formation.Type.NONE;
            //TODO: SHould this reset
            this.ResetAll();
        }
     

    }
}

