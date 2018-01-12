using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAscensor : MonoBehaviour
{
    bool go = false;

    private void Update()
    {
        if (go) { transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime, transform.position.z); }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //TO DO : Sonido de start engine :D
            StartCoroutine(StartGoingDown());
        }
        
    }

    IEnumerator StartGoingDown()
    {
        yield return new WaitForSeconds(5);
        go = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "terreno")
        {
            go = false;
        }
    }
}

