using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Entities;
using RAIN.Action;
using RAIN.Core;

public class S2_Pre_Sigilo_BarroboyMalo : MonoBehaviour {

    Entity Malo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Persona" || other.tag == "Player") { 
        Malo = other.GetComponentInChildren<EntityRig>().Entity;
        Malo.GetAspect("Malo").IsActive = true;
        }

        
        
    }
}
