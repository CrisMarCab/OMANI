using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;

[RAINAction]
public class ActivateRagdoll : RAINAction
{
    GameObject Bone;
    Vector3 bonepos;
    GameObject Objetivo;
    Entity Malo;
    AudioSource[] bases;


    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {

        Objetivo = ai.WorkingMemory.GetItem<GameObject>("varDisparo");

        if (Objetivo.tag == "Player")
        {
            if (Objetivo.GetComponent<BoyMovimiento>().ragdolled == false)
            {
                Bone = Objetivo.transform.GetChild(0).GetChild(0).gameObject;


                Objetivo.GetComponent<BoyMovimiento>().startRagdoll();
                Objetivo.GetComponent<BoyMovimiento>().grabbed = true;

                Bone.GetComponent<Rigidbody>().AddForce(ai.Body.transform.forward * 5000);

                ai.Body.GetComponent<AudioSource>().Play();

                Malo = ai.WorkingMemory.GetItem<GameObject>("varVisto").GetComponentInChildren<EntityRig>().Entity;
                Malo.GetAspect("Malo").MountPoint = Bone.transform;
                Malo.GetAspect("Malo").VisualSize = 0.04f;
                Malo.GetAspect("Malo").PositionOffset = new Vector3(0, 0, 0);


                bases = GameObject.FindWithTag("Brain").GetComponents<AudioSource>();

                foreach (AudioSource b in bases)
                {
                    if (b.clip.name == "Patio de juegos (Base)")
                    {
                        b.Stop();
                    }
                }

                return ActionResult.SUCCESS;
            }
            else
            {

                ai.WorkingMemory.SetItem<bool>("onAction", false);
                return ActionResult.FAILURE;
            }

        }

        else if (Objetivo.tag == "persona")
        {
            Objetivo.gameObject.transform.GetComponentInChildren<Ragdoll>().ragdollTrueNPC();

            Bone = Objetivo.transform.GetChild(0).GetChild(0).gameObject;

            Bone.GetComponent<Rigidbody>().AddForce(ai.Body.transform.forward * 5000);


            ai.Body.GetComponent<AudioSource>().Play();

            Malo = ai.WorkingMemory.GetItem<GameObject>("varVisto").GetComponentInChildren<EntityRig>().Entity;
            Malo.GetAspect("Malo").MountPoint = Bone.transform;
            Malo.GetAspect("Malo").VisualSize = 0.04f;
            Malo.GetAspect("Malo").PositionOffset = new Vector3(0, 0, 0);
            return ActionResult.SUCCESS;
        }
        else
        {

            ai.WorkingMemory.SetItem<bool>("onAction", false);
            return ActionResult.FAILURE;
        }
    }


    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }

}