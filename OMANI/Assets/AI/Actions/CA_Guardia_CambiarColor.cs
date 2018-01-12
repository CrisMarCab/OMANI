using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class CA_Guardia_CambiarColor : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        ai.Body.transform.Find("Armature/Bone/Bone.001/Bone.002/Bone.003/Bone.004/Luz").GetComponent<Light>().color = Color.red;
        ai.Body.transform.Find("Armature/Bone/Bone.001/Bone.002/Bone.003/Bone.004/casco soldados").GetComponent<Renderer>().material.color = Color.red;


        return ActionResult.SUCCESS;

    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}