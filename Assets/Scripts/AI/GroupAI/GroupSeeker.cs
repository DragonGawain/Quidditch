using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSeeker : GroupAI
{
    protected override void OnAwake()
    {
        // string output = "SEEKER " + this.gameObject.name + ", - chasers: ";
        // foreach (GroupAI chaser in friendlyChasers)
        // {
        //     output += chaser.gameObject.name + ", ";
        // }
        // output += "beaters: ";
        // foreach (GroupAI beater in friendlyBeaters)
        // {
        //     output += beater.gameObject.name + ", ";
        // }
        // if (freindlySeeker != null)
        //     output += "seeker: " + freindlySeeker.gameObject.name;
        // if (freindlyKeeper != null)
        //     output += ", keeper: " + freindlyKeeper.gameObject.name;
        // Debug.Log(output);
    }

    // Update is called once per frame
    // protected override void Update()
    // {
    //     base.Update();
    // }
}
