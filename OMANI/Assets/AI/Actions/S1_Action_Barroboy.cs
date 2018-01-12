using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class S1_Action_Barroboy : RAINAction
{
    GameObject mainControl,obj;
	Npc_stats stats;
    Lenguaje lenguaje;
    Rigidbody addforcy;
    public override void Start(RAIN.Core.AI ai)
    {
		stats = ai.Body.GetComponent<Npc_stats> ();
		obj = new GameObject ();
        mainControl = GameObject.Find("Brain");
        lenguaje = mainControl.GetComponent<Lenguaje>();
        base.Start(ai);
        addforcy = ai.Body.GetComponent<Npc_stats>().obj_carry.GetComponent<Rigidbody>();
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        if (ai.WorkingMemory.GetItem<bool>("throw") == false) {
			if (lenguaje.enabled == true) {
				lenguaje.enabled = false;
			}
				mainControl.GetComponent<Basic_BarroAction>().enabled = true;
			mainControl.GetComponent<Basic_BarroAction> ().setup (ai.Body.transform.gameObject, obj);

            


        }else
        {
            stats.obj_carry.GetComponent<Collider>().enabled = true;
			stats.obj_carry.GetComponent<Rigidbody>().useGravity = true;

			stats.obj_carry = null;
			addforcy.AddForce(ai.Body.transform.forward * 400);
			lenguaje.enabled = true;
			mainControl.GetComponent<Basic_BarroAction>().enabled = false;
            ai.WorkingMemory.SetItem("state", "free");
        }
        

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
   
    }