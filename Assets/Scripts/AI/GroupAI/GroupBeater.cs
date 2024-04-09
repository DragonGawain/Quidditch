using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupBeater : GroupAI
{
    protected override void OnAwake()
    {
        // string output = "BEATER " + this.gameObject.name + ", - chasers: ";
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

    private void FixedUpdate()
    {
        if (!isInFormation && stance == AIStance.DEFENSIVE)
            stance = AIStance.AGGRESSIVE;
        else if (isInFormation && stance == AIStance.AGGRESSIVE)
            stance = AIStance.DEFENSIVE;
    }
}
