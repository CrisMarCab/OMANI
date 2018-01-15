using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapToMine : MonoBehaviour {
    [SerializeField] Transform goTo;
    [SerializeField] GameObject CamaraParaLaMina;
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = goTo.position;

            GameObject.Find("PadreCamara").transform.position = goTo.position;

            CamaraParaLaMina.SetActive(true);

            Camera.main.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor ;
        }

       
    }
}
