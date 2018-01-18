using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2_Sigilo_Cristal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("persona")) {
            GetComponentInChildren<Animator>().enabled = false;
            Debug.Log("AnimationStopped");
        }
    }
}
