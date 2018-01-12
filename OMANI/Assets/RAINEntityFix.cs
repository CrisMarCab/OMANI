using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Entities;
using RAIN.Entities.Aspects;

public class RAINEntityFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponentInChildren<EntityRig>().Entity.Form = this.transform.gameObject;
        Destroy(this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
