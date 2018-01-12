using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeofaParticle : MonoBehaviour {
    public GameObject destination;
    void Start()
    {
        StartCoroutine(Destroy());
    }
   void  Update()
    {
        transform.position = Vector3.Lerp(this.transform.position, destination.transform.position, Time.deltaTime*2);
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.transform.gameObject);
    }
}
