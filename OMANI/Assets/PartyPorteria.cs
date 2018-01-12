using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyPorteria : MonoBehaviour {
    ParticleSystem[] particles;
    float time = 2, counter = 0;
    bool count = false;
    private void Start()
    {
        particles = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem part in particles) {
            part.Stop();
        }
    }
    private void Update()
    {
        if (count) {
            counter += Time.deltaTime;
        }

        if (counter > 2)
        {
            count = false;
            counter = 0;

            foreach (ParticleSystem part in particles)
            {
                part.Stop();
            }
        }
    }
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "objeto")
        {
            foreach (ParticleSystem part in particles)
            {
                part.Play();
            }
            count = true;
            GetComponent<AudioSource>().Play();


        }
    }
}
