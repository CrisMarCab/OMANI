using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;
[RAINAction]
public class CA_Ragdoll : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);

    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        ai.Body.gameObject.transform.GetComponentInChildren<Ragdoll>().ragdollTrueNPC();

        var Bone = ai.Body.transform.GetChild(0).GetChild(0).gameObject;

        Bone.GetComponent<Rigidbody>().AddForce(ai.Body.transform.forward * 5000);


        ai.Body.GetComponent<AudioSource>().Play();

        var Malo = ai.WorkingMemory.GetItem<GameObject>("varVisto").GetComponentInChildren<EntityRig>().Entity;
        Malo.GetAspect("Malo").MountPoint = Bone.transform;
        Malo.GetAspect("Malo").VisualSize = 0.04f;
        Malo.GetAspect("Malo").PositionOffset = new Vector3(0, 0, 0);
        
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}