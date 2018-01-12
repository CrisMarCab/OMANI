using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Entities;
using RAIN.Action;
using RAIN.Core;

public class Guardia : MonoBehaviour
{

    public bool watching = false;
    Animator anim;
    public GameObject persona;

    //Surface Hitted
    public RaycastHit hittedground;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {

    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (watching && persona != null)
        {
            anim.SetLookAtWeight(1f, 0.05f, 1f, 0.1f, 0.5f);

            //Position too look at.
            anim.SetLookAtPosition(persona.transform.position);
        }
    }



    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "objeto")
        {
            RaycastHit hit;
            Entity Malo;

            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 10);
            foreach (Collider hitted in hitColliders)
            {
                if (hitted.transform.tag == "persona")
                {

                    Malo = hitted.transform.gameObject.GetComponentInChildren<EntityRig>().Entity;
                    Malo.GetAspect("Malo").IsActive = true;
                }
            }
        }

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 15);
    }

    //Used by the animation events to play the steps.
    void rightStep()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hittedground))
        {
            if (hittedground.collider.gameObject.tag == "Cemento")
            {
                AudioManager.instance.PlayAtPosition("Paso Cemento Derecho", hittedground.transform.position);
            }

            else if (hittedground.collider.gameObject.tag == "Cesped")
            {
                AudioManager.instance.PlayAtPosition("Paso Cesped 1", hittedground.transform.position);
            }
        }
    }
    void leftStep()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hittedground))
        {
            if (hittedground.collider.gameObject.tag == "Cemento")
            {
                AudioManager.instance.PlayAtPosition("Paso Cemento Izquierdo", hittedground.transform.position);
            }
            else if (hittedground.collider.gameObject.tag == "Cesped")
            {
                AudioManager.instance.PlayAtPosition("Paso Cesped 2", hittedground.transform.position);
            }
        }

    }

}
