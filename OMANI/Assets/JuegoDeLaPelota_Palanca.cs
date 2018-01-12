using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuegoDeLaPelota_Palanca : MonoBehaviour {
    public JuegoDeLaPelota juegiko;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "palanca") { 
            if(transform.gameObject.name == "izquierda")
            {
                juegiko.izquierda = true;
            }else if (transform.gameObject.name == "derecha")
            {
                juegiko.derecha = true;
            }
            else if (transform.gameObject.name == "arriba")
            {
                juegiko.arriba = true;
            }
            else if (transform.gameObject.name == "abajo")
            {
                juegiko.abajo = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "palanca")
        {
            if (transform.gameObject.name == "izquierda")
            {
                juegiko.izquierda = false;
            }
            else if (transform.gameObject.name == "derecha")
            {
                juegiko.derecha = false;
            }
            else if (transform.gameObject.name == "arriba")
            {
                juegiko.arriba = false;
            }
            else if (transform.gameObject.name == "abajo")
            {
                juegiko.abajo = false;
            }
        }
    }
}
