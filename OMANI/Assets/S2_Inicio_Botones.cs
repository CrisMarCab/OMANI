using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2_Inicio_Botones : MonoBehaviour
{
    [SerializeField]

    bool tocando = false, abierta = false;
    [SerializeField]
    public GameObject otroBoton, puerta;
    //private Vector3 position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (puerta != null)
        {
            if (tocando == true && otroBoton.GetComponent<S2_Inicio_Botones>().tocando == true && abierta == false)
            {

                puerta.GetComponent<Puerta>().AbrirPuerta();
                abierta = true;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.15f, gameObject.transform.position.z);
        GetComponent<AudioSource>().Play();
    }


    private void OnTriggerStay(Collider other)
    {
        if (gameObject.GetComponent<S2_Inicio_Botones>().enabled)
        {

            if (other.tag == "Player" || other.tag == "persona")
            {
                foreach (Material matt in this.gameObject.GetComponent<MeshRenderer>().materials)
                {
                    if (matt.name == "Laboratorio Pantalla 2 Gris Claro (Instance)")
                    {
                        matt.color = Color.white;

                        matt.EnableKeyword("_EMISSION");

                        Color baseColor = Color.white; //Replace this with whatever you want for your base color at emission level '1'

                        Color finalColor = baseColor * Mathf.LinearToGammaSpace(1);

                        matt.SetColor("_EmissionColor", finalColor);

                    }
                }
                tocando = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.GetComponent<S2_Inicio_Botones>().enabled)
        {

            if (other.tag == "Player" || other.tag == "persona")
            {
                foreach (Material matt in this.gameObject.GetComponent<MeshRenderer>().materials)
                {
                    if (matt.name == "Laboratorio Pantalla 2 Gris Claro (Instance)")
                    {
                        matt.color = Color.grey;

                        matt.DisableKeyword("_EMISSION");

                    }
                }

                tocando = false;
            }
        }

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.15f, gameObject.transform.position.z);

    }
}
