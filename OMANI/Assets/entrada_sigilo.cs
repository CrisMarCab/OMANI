using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entrada_sigilo : MonoBehaviour
{
    [SerializeField]
    GameObject[] objectstodisable = new GameObject[10];
    [SerializeField]
    GameObject sigiloposition, posicionBasura, basuraS2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" || other.tag == "persona")
        {
            if (this.name == "EntradaSigilo")
            {
                if (other.tag == "Player")
                {
                    FindObjectOfType<ControlCamara>().StaticCamera = true;

                    foreach (GameObject objs in objectstodisable)
                    {
                        objs.SetActive(false);
                    }
                    basuraS2.transform.SetParent(posicionBasura.transform);

                    basuraS2.transform.localRotation = Quaternion.identity;
                    basuraS2.transform.localPosition = new Vector3(0,0,0);

                }

                other.transform.position = sigiloposition.transform.position;
            }

        }


    }
}