using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portero : MonoBehaviour {
    float timer;
    bool mirror = false;
    Animator anim;
    // Use this for initialization
    void Start () {
        anim = this.gameObject.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        timer += Time.deltaTime;
        if (timer > 2) {
            timer = 0;
            if (mirror == true)
            {
                anim.SetBool("Mirror", false);
                mirror = false;
            }
            else {

                anim.SetBool("Mirror", true);
                mirror = true;
            }
        }
	}
}
