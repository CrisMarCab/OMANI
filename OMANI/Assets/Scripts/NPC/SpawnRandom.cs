using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RAIN.Action;
using RAIN.Core;

public class SpawnRandom : MonoBehaviour {

  
    //Spawn this object
    public GameObject spawnObject;

    public float maxTime ;
    public float minTime ;

    //current time
    private float time;

    //The time to spawn the object
    private float spawnTime;

    void Start()
    {
        SetRandomTime();
        time = 0;
    }

    void FixedUpdate()
    {

        //Counts up
        time += Time.deltaTime;

        //Check if its the right time to spawn the object
        if (time >= spawnTime)
        {
            SpawnObject();
            SetRandomTime();
        }

    }


    //Spawns the object and resets the time
    void SpawnObject()
    {
        time = 0;
        GameObject spawneado = Instantiate(spawnObject, transform.position, spawnObject.transform.rotation);
        spawneado.GetComponentInChildren<AIRig>().AI.Body = spawneado;
    }

    //Sets the random time between minTime and maxTime
    void SetRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }
}
