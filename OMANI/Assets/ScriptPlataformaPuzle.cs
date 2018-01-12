using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlataformaPuzle : MonoBehaviour {
    [SerializeField] ScriptMovimientoPuzle puzle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	
    private void OnTriggerEnter(Collider other)
    {
        puzle.go = true;
        GetComponent<AudioSource>().Play();
    }
    private void OnTriggerExit(Collider other)
    {
       
    }
}
