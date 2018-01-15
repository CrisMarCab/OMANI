using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_CambioCamara : MonoBehaviour {

    [SerializeField] GameObject CamaraDeLaZona;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { CamaraDeLaZona.SetActive(true);  }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") { CamaraDeLaZona.SetActive(false); }
    }


}
