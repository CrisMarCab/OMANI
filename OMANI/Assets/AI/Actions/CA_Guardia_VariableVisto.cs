using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class CA_Guardia_VariableVisto : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);

        GameObject ultimapersonavista = (GameObject)ai.WorkingMemory.GetItem("varVisto");
        ai.WorkingMemory.SetItem("variableVisto", ultimapersonavista.transform.position);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}