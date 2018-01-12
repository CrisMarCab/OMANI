using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puertas : MonoBehaviour {
    public GameObject puertaIzquierda,puertaDerecha;
    public GameObject  posicionFinalIz, posicionFinalDer;
    Vector3 posicionIz, posicionDer;
    bool open = false;
    float speed = 2;
	// Use this for initialization
	void Start () {
        posicionIz = puertaIzquierda.transform.position;
        posicionDer = puertaDerecha.transform.position;
    }

    private void Update()
    {
        if (open)
        {
            puertaIzquierda.transform.position = Vector3.Lerp(puertaIzquierda.transform.position, posicionFinalIz.transform.position,Time.deltaTime * speed);
            puertaDerecha.transform.position = Vector3.Lerp(puertaDerecha.transform.position, posicionFinalDer.transform.position, Time.deltaTime * speed);

        }
        else
        {

            puertaIzquierda.transform.position = Vector3.Lerp(puertaIzquierda.transform.position, posicionIz, Time.deltaTime * speed);
            puertaDerecha.transform.position = Vector3.Lerp(puertaDerecha.transform.position, posicionDer, Time.deltaTime * speed);

        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("quepaa");
        open = true;

        GetComponent<AudioSource>().Play();
    }
    private void OnTriggerExit(Collider other)
    {
        open = false;
        GetComponent<AudioSource>().Play();
    }
}
