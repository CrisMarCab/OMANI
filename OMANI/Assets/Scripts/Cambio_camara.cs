using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cambio_camara : MonoBehaviour {
    [SerializeField]
    GameObject Player, Brain;
    AudioSource[] bases;
    AudioSource basesource;
    [SerializeField]
    float sound;
    bool sound_dinamic = true;

	// Use this for initialization
	void Start () {
        bases = FindObjectOfType<AudioManager>().GetComponents<AudioSource>();

        foreach (AudioSource b in bases) {
            if (b.clip.name == "Patio de juegos (Base)") {
                basesource = b;
            }
        }
        
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (sound_dinamic) {
            sound = Vector3.Distance(Player.transform.position, this.transform.position);
            sound = -(sound - 48) / 48;

            basesource.volume = sound; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="MainCamera") {
            FindObjectOfType<ElementosPasillo>().lightreduction = true;
            StartCoroutine(FindObjectOfType<ElementosPasillo>().DesactivarPasillo());
            sound_dinamic = false;
            this.gameObject.transform.position = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z - 0.2f);
        }
    }
}
