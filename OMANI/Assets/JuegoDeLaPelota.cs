using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuegoDeLaPelota : MonoBehaviour {
    public bool izquierda = false, derecha = false, arriba = false, abajo = false;
    public float velocity;
    // Use this for initialization
    void Update()
    {
        if (izquierda)
        {
            Quaternion previous = transform.rotation;
            transform.rotation = Quaternion.AngleAxis(velocity, Vector3.up) * previous;

            //transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y+velocity, transform.eulerAngles.z);
        }
        else if (derecha)
        {
            Quaternion previous = transform.rotation;
            transform.rotation = Quaternion.AngleAxis(-velocity, Vector3.up) * previous;
            //transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y -velocity, transform.eulerAngles.z);
        }
        else if(arriba)
        {
            Quaternion previous = transform.rotation;
            transform.rotation = Quaternion.AngleAxis( velocity, Vector3.forward) * previous;
            //transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, transform.eulerAngles.z +velocity);
        }
        else if(abajo)
        {
            Quaternion previous = transform.rotation;
            transform.rotation = Quaternion.AngleAxis(- velocity, Vector3.forward) * previous;
            //transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, transform.eulerAngles.z - velocity);
        }
    }
}
