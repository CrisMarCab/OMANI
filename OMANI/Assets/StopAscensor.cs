using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAscensor : MonoBehaviour {

    [SerializeField] ScriptAscensor Ascensoriko;
    [SerializeField] GameObject DesactivarCamara, ActivarCamara;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            Ascensoriko.go = false;
            DesactivarCamara.SetActive(false);
            ActivarCamara.SetActive(true);
            Destroy(this);
        }
    }

}
