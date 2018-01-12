using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuegoDeLaPelota : MonoBehaviour {
    public bool izquierda = false, derecha = false, arriba = false, abajo = false;
    public Transform rizquierda, rderecha, rarriba, rabajo;
    public float velocity;
    // Use this for initialization
    void FixedUpdate()
    {
        if (izquierda)
        {

            transform.rotation = Quaternion.Lerp(transform.rotation,rizquierda.rotation, velocity);
        }else if (derecha)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rderecha.rotation, velocity);
        }
        else if(arriba)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rarriba.rotation, velocity);
        }
        else if(abajo)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rabajo.rotation, velocity);
        }
    }
}
