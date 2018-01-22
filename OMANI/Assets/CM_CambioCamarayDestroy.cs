using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_CambioCamarayDestroy : MonoBehaviour {


    [SerializeField] GameObject CamaraDeLaZona;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            CamaraDeLaZona.SetActive(true);
            Destroy(this);
        }
    }

   


}
