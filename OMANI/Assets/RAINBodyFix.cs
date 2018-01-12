using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RAIN.Action;
using RAIN.Core;

public class RAINBodyFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.gameObject.GetComponentInChildren<AIRig>().AI.Body = this.gameObject;
       
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.gameObject.GetComponentInChildren<AIRig>().AI.Body == this.gameObject)
        {
            Destroy(this);

        }
        else
        {
            transform.gameObject.GetComponentInChildren<AIRig>().AI.Body = this.gameObject;
        }
    }
}
