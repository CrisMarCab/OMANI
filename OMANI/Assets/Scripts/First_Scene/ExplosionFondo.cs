using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFondo : MonoBehaviour
{
    // Use this for initialization
    [SerializeField]
    private float timeExplode;

    [SerializeField]
    public GameObject barroboyNPC, go, explosionSuperior, explosionCentral, explosionInferior;

    void Start()
    {
        timeExplode = Random.Range(20, 60);
        StartCoroutine(FindObjectOfType<AudioManager>().Play("fondo_Cristal", timeExplode - 1f));
        StartCoroutine(FindObjectOfType<AudioManager>().Play("fondo_ExplosionCristal", timeExplode - 0.55f));
        Invoke("Instantiate(explosionInferior)", timeExplode - 1f);
        StartCoroutine(Born());

    }


    public IEnumerator Born()
    {
        yield return new WaitForSeconds(timeExplode);
        Transform[] ts = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform child in ts)
        {
            if (child.transform.name == "Icosphere")
            {
                Destroy(child.transform.gameObject);

                Instantiate(go, new Vector3(child.transform.position.x, child.transform.position.y + 6, child.transform.position.z), new Quaternion(child.transform.rotation.x, child.transform.rotation.y, child.transform.rotation.z, 0));

                Instantiate(explosionCentral, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 4, gameObject.transform.position.z), Quaternion.identity);

                Instantiate(explosionSuperior, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 13, gameObject.transform.position.z), Quaternion.identity);

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
        GameObject instNPC = Instantiate(barroboyNPC, this.gameObject.transform);
        StartCoroutine(instNPC.transform.GetComponentInChildren<Ragdoll>().ragdollTrueNPC(0.05f));


    }


}
