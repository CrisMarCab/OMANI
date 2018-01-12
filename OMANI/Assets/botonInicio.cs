using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botonInicio : MonoBehaviour {

    public string cristal;
    public GameObject Activate;
    [SerializeField]
    camara_MindControl control;

    // Update is called once per frame
    private void Start()
    {
        Activate = GameObject.Find(cristal);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "palanca") {
            Activate.GetComponent<Explosion>().enabled = true;
            control.goBacktoNormal();
        }
    }
}
