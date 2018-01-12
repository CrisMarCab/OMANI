using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantaQueSeEncoge : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "persona" || other.tag == "Player") { 
        this.gameObject.GetComponent<Animator>().SetTrigger("Colision");
        this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
