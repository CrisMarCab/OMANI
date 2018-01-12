using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaBasura : MonoBehaviour
{

    [SerializeField]
    GameObject puerta;

    Animator[] animators = new Animator[2];

    // Use this for initialization
    void Start()
    {
        animators = puerta.GetComponentsInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AbrirPuertaBasura()
    {

        for (int i = 0; i < 2; i++)
        {
            animators[i].enabled = true;
        }

        this.gameObject.GetComponent<AudioSource>().Play();
    }

    public void CerrarPuertaBasura()
    {
        for (int i = 0; i < 2; i++)
        {
            animators[i].enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AbrirPuertaBasura();
    }
    private void OnTriggerExit(Collider other)
    {
        //  StartCoroutine();
    }

}
