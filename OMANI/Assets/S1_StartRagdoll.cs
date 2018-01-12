using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S1_StartRagdoll : MonoBehaviour {

    private GameObject Bone, Barroboy;
    Vector3 bonepos, armapos;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Player") {
            Barroboy = other.gameObject;

            Bone = other.transform.GetChild(0).GetChild(0).gameObject;
            Barroboy.GetComponent<BoyMovimiento>().startRagdoll();
            Bone.GetComponent<Rigidbody>().AddForce(Barroboy.transform.forward * 2500);

            StartCoroutine(changePosition());
        }
    }

    public IEnumerator changePosition() {
        yield return new WaitForSeconds(1.5f);

        bonepos = Bone.transform.TransformPoint(0, 0, 0);
        Barroboy.transform.position = bonepos;
        Bone.transform.position = bonepos;

    }

}
