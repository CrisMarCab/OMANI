using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class CA_easyThrow : RAINAction
{

    GameObject mainControl, obj;
    Npc_stats stats;
    Lenguaje lenguaje;
    Rigidbody addforcy;
    float fuerza = 300;

    public override void Start(RAIN.Core.AI ai)
    {
        stats = ai.Body.GetComponent<Npc_stats>();
        addforcy = ai.Body.GetComponent<Npc_stats>().obj_carry.GetComponent<Rigidbody>();
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        if (stats.obj_carry.transform.tag == "objeto")
        {
            fuerza = 700;
            stats.obj_carry.GetComponent<Collider>().enabled = true;
        }

        addforcy.useGravity = true;
        addforcy.isKinematic = false;

        stats.obj_carry = null;
        addforcy.AddForce(ai.Body.transform.forward * fuerza);

        if (ai.Body.gameObject.name == "NpcThrower")
        {
            ai.WorkingMemory.SetItem("throw", false);
            AIRig buddy = ai.WorkingMemory.GetItem<GameObject>("Buddy").GetComponentInChildren<AIRig>();
            if (buddy != null)
            {
                buddy.AI.WorkingMemory.SetItem("throw", true);
            }
        }

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}