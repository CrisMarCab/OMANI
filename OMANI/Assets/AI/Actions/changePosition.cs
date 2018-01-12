using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class changePosition : RAINAction
{
    GameObject Bone, Barroboy, Armature;
    Vector3 bonepos, armapos;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        Barroboy = ai.WorkingMemory.GetItem<GameObject>("varDisparo");
        Armature = Barroboy.transform.GetChild(0).gameObject;

        Bone = Barroboy.transform.GetChild(0).GetChild(0).gameObject;
        GameObject disparo_particulas = ai.Body.transform.Find("Armature/Bone/Bone.001/Bone.002/Bone.007/Bone.008/Bone.008_end/Pistola/Point/Diisparo").gameObject;
        disparo_particulas.GetComponent<ParticleSystem>().Play();
        bonepos = Bone.transform.TransformPoint(0,0,0);
        //armapos = Armature.transform.TransformPoint(0,0,0);

        Barroboy.transform.position = bonepos;
        Bone.transform.position = bonepos;

    
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}