using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapToMine : MonoBehaviour {
    [SerializeField] Transform goTo;
    [SerializeField] GameObject CamaraParaLaMina,luzDirectional1,luzDireccional2;
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = goTo.position;
            

            CamaraParaLaMina.SetActive(true);

            Camera.main.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor ;

            luzDirectional1.SetActive(false);

            luzDireccional2.SetActive(false);
        }

       
    }
}
