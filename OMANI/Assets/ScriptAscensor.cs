using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAscensor : MonoBehaviour
{
    public bool go = false;
    [SerializeField] GameObject CamaraStatica;

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
        yield return new WaitForSeconds(3);
        CamaraStatica.SetActive(true);
        go = true;
    }
   
}

