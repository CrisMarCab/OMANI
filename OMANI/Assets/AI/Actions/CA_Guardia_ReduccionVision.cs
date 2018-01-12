using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class CA_Guardia_ReduccionVision : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    
        ai.Senses.GetSensor("Vision").PositionOffset = new Vector3(0, 0, 0);

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