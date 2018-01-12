using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class CA_ComprobarObj : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        if (ai.WorkingMemory.GetItem<GameObject>("objective").tag == "persona")
        {
            ai.WorkingMemory.SetItem("ObjPersona",true);
        }else
        {
            ai.WorkingMemory.SetItem("ObjPersona", false);
        }
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}