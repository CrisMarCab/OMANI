using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Barroboy : MonoBehaviour {
    public float strength;

    // Update is called once per frame
    public void action(GameObject objeto1,Transform objetivo) {
        objeto1.GetComponent<Rigidbody>().AddForce((objetivo.position-objeto1.transform.position).normalized*strength);
    }
}
