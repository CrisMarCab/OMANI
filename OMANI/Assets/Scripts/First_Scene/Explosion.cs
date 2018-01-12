using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public int time;
    // Use this for initialization
    public GameObject go, explosionSuperior, explosionCentral, explosionInferior, barroBoy, ignorar, instantiatedBarroboy, explosion;
    private GameObject padrecamara;
    private ControlCamara controlCam;

    void Start()
    {
        StartCoroutine(FindObjectOfType<AudioManager>().Play("Cristal", 4f));
        StartCoroutine(FindObjectOfType<AudioManager>().Play("FondoCristal", 4.45f));
        StartCoroutine(FindObjectOfType<AudioManager>().Play("ExplosionCristal", 4.55f));

        StartCoroutine(Born());
        StartCoroutine(HumoInferior());

        padrecamara = GameObject.Find("PadreCamara");

    }

    public IEnumerator HumoInferior()
    {
        yield return new WaitForSeconds(time - 4f);

        explosion = Instantiate(explosionInferior, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z), Quaternion.identity);
    }


    public IEnumerator Born()
    {
        yield return new WaitForSeconds(time);
        Transform[] ts = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform child in ts)
        {
            if (child.transform.name == "Icosphere")
            {
                go.transform.position = new Vector3(child.transform.position.x, child.transform.position.y + 6, child.transform.position.z);
                go.transform.rotation = new Quaternion(child.transform.rotation.x, child.transform.rotation.y, child.transform.rotation.z, 0);
                Destroy(child.transform.gameObject);

                Instantiate(go);
                Instantiate(explosionCentral, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 4, gameObject.transform.position.z), Quaternion.identity);

                Instantiate(explosionSuperior, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 13, gameObject.transform.position.z), Quaternion.identity);


                Invoke("barroBoyInstance", 0.5f);
                Invoke("barroboyKinematic", 0.6f);



            }

            else if (child.transform.name == "Barroboy_Incubadora")
            {
                Destroy(child.transform.gameObject);
            }
            else if (child.transform.name == "Point light")
            {
                Destroy(child.transform.gameObject);
            }
        }

    }

    public void barroBoyInstance()
    {
        instantiatedBarroboy = Instantiate(barroBoy, this.gameObject.transform.Find("SpawnPosition"));
        controlCam.state = 1;
        controlCam.PlayerPosition = instantiatedBarroboy;
    }

    public void barroboyKinematic() {
        instantiatedBarroboy.GetComponent<Rigidbody>().isKinematic = true;
        instantiatedBarroboy.GetComponent<Collider>().enabled = false;
        FindObjectOfType<BoyMovimiento>().anim.enabled = false;
        FindObjectOfType<BoyMovimiento>().ragdolled = true;

    }
    private void OnEnable()
    {
        controlCam = FindObjectOfType<ControlCamara>();
        controlCam.StaticCamera = false;
        controlCam.PlayerPosition = this.gameObject;
        controlCam.state = 3;
        controlCam.CameraWatchingPlayer = true;
        disableSystem();
    }
    void disableSystem() {
        var todoslosScripts = FindObjectsOfType<botonInicio>();
        foreach (botonInicio exp in todoslosScripts)
        {
            if (exp.Activate == this.transform.gameObject)
            {
                exp.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
                
            }
            else
            {
                exp.transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }

            Destroy(exp);
        }



    }

}
