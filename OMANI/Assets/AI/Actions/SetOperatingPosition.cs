using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class SetOperatingPosition : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        var pos = ai.Body;
        pos.GetComponent<Npc_stats>().lever = true;
        
        return ActionResult.SUCCESS;
    }
    

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}
