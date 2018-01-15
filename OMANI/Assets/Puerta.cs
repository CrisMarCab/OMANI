using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour {

    Vector3 startPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AbrirPuerta() {
        this.GetComponent<Animation>().Play();
        this.GetComponent<AudioSource>().Play();
    }

    private void LateUpdate()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            AbrirPuerta();
        }
        Debug.Log("Entrado");
    }
}
