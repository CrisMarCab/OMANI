using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCollision : MonoBehaviour {
    [SerializeField] Transform ObjetoQueVaAMirar;
    // Use this for initialization

    AudioSource audio; 
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "persona")
        {
            ObjetoQueVaAMirar.LookAt(other.transform);
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        audio.Play();
    }
    private void OnTriggerExit(Collider other)
    {
        audio.Stop();
    }
}
