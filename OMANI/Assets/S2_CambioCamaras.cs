
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2_CambioCamaras : MonoBehaviour
{

    [SerializeField]
    GameObject cameraposition, cameraposition2, luz1, luz2, pasillo;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {

        if (this.gameObject.name == "CambioCamara" && other.tag == "Player")
        {
            FindObjectOfType<ControlCamara>().state = 7;
            //Camera.main.transform.position = cameraposition2.transform.position;
            //Camera.main.transform.rotation = cameraposition2.transform.rotation;

        }

        if (this.gameObject.name=="CambioCamara2" && other.tag == "Player")
        {
            FindObjectOfType<ControlCamara>().state = 0;
            //Camera.main.transform.position = cameraposition.transform.position;
            //Camera.main.transform.rotation = cameraposition.transform.rotation;
            luz2.SetActive(true);

            luz1.SetActive(true);
            pasillo.SetActive(true);
        }
    }
}
