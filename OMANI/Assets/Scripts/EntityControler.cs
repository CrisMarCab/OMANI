using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;
using RAIN.Action;
using RAIN.Entities;
using RAIN.Entities.Aspects;


public class EntityControler : MonoBehaviour {
	private Lenguaje scriptcentral;
	public GameObject VeryImportantPerson;
	// Use this for initialization
	void Start () {	
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(VeryImportantPerson != null){
			if (VeryImportantPerson.GetComponentInChildren<AIRig>().AI.WorkingMemory.GetItem("objective")==this.gameObject) {
				if (!GetComponentInChildren<EntityRig> ()) {
					//SI NO TIENE ENTITY RIG LO CREAMOS Y LE AÑADIMOS EL ASPECTO DE OBJECTIVE
					EntityRig tNewRig = EntityRig.AddRig (this.transform.gameObject);
					VisualAspect tAspect = new VisualAspect ();
					tAspect.AspectName = VeryImportantPerson.GetComponent<Npc_stats>().namePlusId;
					tNewRig.Entity.AddAspect (tAspect);
				} else {
					//SI TIENE ENTITY ENCONTRAMOS EL ASPECTO OBJECTIVE Y LO PONEMOS EN TRUE
					EntityRig tNewRig = (EntityRig)GetComponentInChildren<EntityRig>();
					VisualAspect tAspect = (VisualAspect)tNewRig.Entity.GetAspect (VeryImportantPerson.GetComponent<Npc_stats>().namePlusId);
					if (tAspect != null) { //si hemos encontrado el aspecto
						if (!tAspect.IsActive) {
							tAspect.IsActive = true; //lo ponemos en true
						}
					} else { //si no lo creamos y ya
						tAspect = new VisualAspect ();
						tAspect.AspectName = VeryImportantPerson.GetComponent<Npc_stats>().namePlusId;
						tNewRig.Entity.AddAspect (tAspect);
					}

				}
			}else { //si no somos el objetivo de veryImportantperson desactivamos el Aspecto de objetivo
				if (GetComponentInChildren<EntityRig> ()) {
					
					EntityRig tNewRig = GetComponentInChildren<EntityRig>();
					VisualAspect tAspect = (VisualAspect)tNewRig.Entity.GetAspect (VeryImportantPerson.GetComponent<Npc_stats>().namePlusId);
					if (tAspect.IsActive){
						tAspect.IsActive = false;
					}
                    Destroy(this);
				}

			}
		}


	}
}
