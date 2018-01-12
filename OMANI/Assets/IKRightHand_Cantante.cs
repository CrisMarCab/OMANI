using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKRightHand_Cantante : MonoBehaviour
{
    [SerializeField]
     Transform Micro;
    [SerializeField]
    Animator anim;
    // Use this for initialization
    private void OnAnimatorIK()
    {
        //aiRig.AI.WorkingMemory.GetItem<GameObject>("objective").transform.Find("level").transform.position Esto es lo que dice mi cabessa
        
            anim.SetIKPosition(AvatarIKGoal.RightHand, Micro.position);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.8f);
        

    }
}
