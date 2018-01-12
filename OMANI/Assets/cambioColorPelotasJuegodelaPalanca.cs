using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambioColorPelotasJuegodelaPalanca : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "lever") {
            collision.gameObject.GetComponent<Renderer>().material.color = Color.Lerp(collision.gameObject.GetComponent<Renderer>().material.color, Color.red, 0.1f);
        }
    }

}
