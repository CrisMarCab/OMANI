using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHit : MonoBehaviour {

    public GameObject[] wall, laboratory, forestbackground, ceilings;
    private GameObject Bone, Barroboy;
    Vector3 bonepos;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "Player") { 
        Barroboy = GameObject.FindGameObjectWithTag("Player");

        Bone = Barroboy.transform.GetChild(0).GetChild(0).gameObject;

        Barroboy.GetComponent<BoyMovimiento>().startRagdoll();
        Bone.GetComponent<Rigidbody>().AddForce((-Barroboy.transform.forward) * 3000);

        this.gameObject.GetComponent<AudioSource>().Play();
        }

        foreach (GameObject ceiling in ceilings)
        {
            ceiling.SetActive(true);
        }

        foreach (GameObject currentWall in wall)
        {
            currentWall.SetActive(true);
        }
        foreach (GameObject currentLaboratory in laboratory)
        {

            currentLaboratory.SetActive(true);
        }
        foreach (GameObject currentForestbackground in forestbackground)
        {

            currentForestbackground.SetActive(false);
        }

        StartCoroutine(OriginalState());
    }

    public IEnumerator OriginalState()
    {
        yield return new WaitForSeconds(4f);
        foreach (GameObject currentWall in wall)
        {

            currentWall.SetActive(false);
        }
        foreach (GameObject currentLaboratory in laboratory)
        {

            currentLaboratory.SetActive(false);
        }

        foreach (GameObject ceiling in ceilings)
        {
            ceiling.SetActive(false);
        }

        foreach (GameObject currentForestbackground in forestbackground)
        {
            currentForestbackground.SetActive(true);
        }



    }

    
}
