using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nave2D : MonoBehaviour {

    bool piloto = false, player = false;
    [SerializeField] AudioSource error,motor;
	// Update is called once per frame
	void Update () {
		if (piloto && player)
        {

        }
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "persona") {
            piloto = true;
            collision.gameObject.SetActive(false);
        }
        if (collision.transform.tag == "Player")
        {
            if (piloto)
            {
                collision.gameObject.SetActive(false);
                player = true;
                motor.Play();

            }else
            {
                error.Play();
            }
        }
    }
}
