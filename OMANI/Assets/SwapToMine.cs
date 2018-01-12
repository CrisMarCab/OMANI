using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapToMine : MonoBehaviour {
    [SerializeField] Transform goTo;
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = goTo.position;
            GameObject.Find("PadreCamara").transform.position = goTo.position;
            Camera.main.transform.position = GameObject.Find("PadreCamara").transform.Find("Diablo").transform.position;
            Camera.main.transform.rotation = GameObject.Find("PadreCamara").transform.Find("Diablo").transform.rotation;
            GameObject.Find("PadreCamara").GetComponent<ControlCamara>().state = 1;
            Camera.main.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor ;
        }

       
    }
}
