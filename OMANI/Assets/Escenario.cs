using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escenario : MonoBehaviour {
    [SerializeField]
    public GameObject FocoIzquierda,FocoDerecha;

    [SerializeField]
    public Transform FizquierdaArriba, FizquierdaAbajo, FDerechaArriba, FDerechaAbajo;

    [SerializeField]
    float velocity;
    private void Start()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "palanca") { 
            if(transform.gameObject.name == "DerechaArriba")
            {
                FocoDerecha.transform.rotation= Quaternion.Lerp(FocoDerecha.transform.rotation, FDerechaArriba.rotation, velocity);
            }
            else if (transform.gameObject.name == "DerechaAbajo")
            {
                FocoDerecha.transform.rotation = Quaternion.Lerp(FocoDerecha.transform.rotation, FDerechaAbajo.rotation, velocity);
            }
            else if (transform.gameObject.name == "IzquierdaArriba")
            {
                FocoIzquierda.transform.rotation = Quaternion.Lerp(FocoIzquierda.transform.rotation, FizquierdaArriba.rotation, velocity);
            }
            else if (transform.gameObject.name == "IzquierdaAbajo")
            {
                FocoIzquierda.transform.rotation = Quaternion.Lerp(FocoIzquierda.transform.rotation, FizquierdaAbajo.rotation, velocity);
            }
        }
    }
    
}
