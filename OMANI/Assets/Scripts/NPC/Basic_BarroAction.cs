using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;
using RAIN.Action;
using RAIN.Entities;
using RAIN.Entities.Aspects;


public class Basic_BarroAction : MonoBehaviour {
	public ParticleLink particles;
	GameObject mainControl;
	public GameObject npcThrowing,direction, player,camarapadre;
	ControlCamara camara;
	// Use this for initialization
	void Start () {
		//get values of everything
		mainControl = GameObject.Find("Brain");
		camara = camarapadre.GetComponent<ControlCamara> ();
		player = camara.PlayerPosition;
		camara.PlayerPosition = npcThrowing;
	}

	void FixedUpdate () {
		//we makes sure both variables are initialized
		if (npcThrowing != null &&  direction != null){
			
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100))
		{

				direction.transform.position = hit.point; //transform the direction "placeholder GO" to mouse position
				npcThrowing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("objetive",direction);
				particles.cosaRaton (npcThrowing,"orden");
				if (Input.GetMouseButtonDown (0)){ //this cancels all

					camara.PlayerPosition = player; //camara restore to normal
					npcThrowing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("objective",null); //restore objective no null
					npcThrowing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<string> ("state","free"); //memory reseted


					npcThrowing.GetComponent<Npc_stats>().obj_carry.GetComponent<Collider>().enabled = true; //activate mesh collider
					npcThrowing.GetComponent<Npc_stats>().obj_carry.GetComponent<Rigidbody>().useGravity = true; //use gravity

					npcThrowing.GetComponent<Npc_stats>().obj_carry = null; // stop carrying
					npcThrowing = null;
					direction = null;
					mainControl.GetComponent<Lenguaje>().enabled = true; //enable normal controls
					mainControl.GetComponent<Lenguaje>().selected = null;
					particles.liberar (); //reset particle system.
					this.enabled = false; //diable all this


				} else if (Input.GetMouseButtonDown (1)) //throw
				{

					camara.PlayerPosition = player; //restore camara to standard position
					npcThrowing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<bool> ("throw",true); //set variable throw to 
					//true, that makes the behabiour tree advance to next constraint, wich will make the object be thrown
					mainControl.GetComponent<Lenguaje>().selected = null;
					particles.liberar (); //reset particle system.
				}
		}
		}
	}
	public void setup(GameObject pthrowing, GameObject pObj){
		this.npcThrowing = pthrowing;
		this.direction = pObj;
	}
}
