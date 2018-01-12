using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class CA_Guardia_CambiarColor_naranja : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {



        if (ai.WorkingMemory.GetItem<GameObject>("varPersona") != null && ai.WorkingMemory.GetItem<GameObject>("varVisto") == null)
        {
        ai.Body.transform.Find("Armature/Bone/Bone.001/Bone.002/Bone.003/Bone.004/Luz").GetComponent<Light>().color = Color.yellow;
        ai.Body.transform.Find("Armature/Bone/Bone.001/Bone.002/Bone.003/Bone.004/casco soldados").GetComponent<Renderer>().material.color = Color.yellow;
        ai.Body.transform.GetComponent<Guardia>().persona = ai.WorkingMemory.GetItem<GameObject>("varPersona");
        ai.Body.transform.GetComponent<Guardia>().watching = true;
        }

        else
        {
            ai.Body.transform.Find("Armature/Bone/Bone.001/Bone.002/Bone.003/Bone.004/Luz").GetComponent<Light>().color = Color.green;
            ai.Body.transform.Find("Armature/Bone/Bone.001/Bone.002/Bone.003/Bone.004/casco soldados").GetComponent<Renderer>().material.color = Color.green;
        }
        return ActionResult.SUCCESS;

    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}