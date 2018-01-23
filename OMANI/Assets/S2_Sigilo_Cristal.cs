using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;

public class S2_Sigilo_Cristal : MonoBehaviour
{
    [Header("GameObject")]

    [SerializeField]
    private GameObject piedra, posicionGuardia;
    [SerializeField]
    private GameObject[] Guardias_Alarmados = new GameObject[5], luces_normales, luces_rojas;
    [Space(10, order = 0)]

    [Header("Logica")]

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
        //Provisional mientras no es un boton.

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

        //Si entra el cristal, hay energia.
        if (collision.gameObject.CompareTag("objeto")) {

            foreach (GameObject luz in luces_normales)
            {
                luz.SetActive(true);
            }

            foreach (GameObject luz in luces_rojas)
            {
                luz.SetActive(false);
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

            foreach (GameObject guardia in Guardias_Alarmados)
            {
                guardia.GetComponent<Animator>().SetTrigger("LookAround");
                guardia.GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem<Vector3>("variableVisto", posicionGuardia.transform.position);
            }

            foreach (GameObject luz in luces_normales)
            {
                luz.SetActive(false);
            }

            foreach (GameObject luz in luces_rojas)
            {
                luz.SetActive(true);
            }

        }
    }
}
