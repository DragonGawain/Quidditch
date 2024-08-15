using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupBeater : GroupAI
{
   


    protected override void FixedUpdate()
    {
        if (!InFormation && stance == groupAI.stance.DEFENSIVE)
            stance = groupAI.stance.AGGRESSIVE;
        else if (InFormation && stance == groupAI.stance.AGGRESSIVE)
            stance = groupAI.stance.DEFENSIVE;

        base.FixedUpdate();
    }
}
