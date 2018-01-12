using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;

[RAINAction]
public class Pickup : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        ai.Body.GetComponent<Npc_stats>().obj_carry = ai.WorkingMemory.GetItem<GameObject>("objective");

        if (ai.Body.GetComponent<Npc_stats>().obj_carry.GetComponentInChildren<EntityRig>().Entity.GetAspect("Malo") != null)
        {
            ai.Body.GetComponent<Npc_stats>().obj_carry.transform.GetChild(0).GetComponent<Ragdoll>().ragdolledPickup();
            ai.Body.GetComponent<Npc_stats>().obj_carry.GetComponentInChildren<EntityRig>().Entity.GetAspect("Malo").IsActive = false;
        }

        ai.Body.GetComponent<Npc_stats>().obj_carry.GetComponent<Collider>().enabled = false;
        ai.Body.GetComponent<Npc_stats>().obj_carry.GetComponent<Rigidbody>().useGravity = false;


        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}