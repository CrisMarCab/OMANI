using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2_Sigilo_Cristal : MonoBehaviour
{
    [SerializeField]
    private GameObject piedra;
    [SerializeField]
    private bool animada, cristalEnZona;
    [SerializeField]
    private float contador, distancia, velocidad = 0.05f, tiempoInicio;
    private Vector3 startposition;

    private void Awake()
    {
        startposition = piedra.transform.position;

    }

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        contador += Time.deltaTime;

        if (animada == true)
        {

            piedra.GetComponent<Rigidbody>().useGravity = false;

            distancia = Vector3.Distance(piedra.transform.position, startposition);

            float distCovered = (Time.time - tiempoInicio) * velocidad;
            float fracJourney = distCovered / distancia;

            piedra.transform.position = Vector3.Lerp(piedra.transform.position, startposition, fracJourney);

            if (cristalEnZona)
            {
                piedra.GetComponent<Animator>().enabled = animada;
            }
        }

        else
        {
            piedra.GetComponent<Animator>().enabled = animada;
            piedra.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("persona"))
        {
            if (animada == true && contador > 3)
            {
                animada = false;
                contador = 0;
            }
            else
            {
                if (contador > 3)
                {
                    animada = true;
                    contador = 0;
                    tiempoInicio = Time.time;
                }
            }
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("objeto"))
        {
            cristalEnZona = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("objeto"))
        {

            cristalEnZona = false;
        }
    }
}
