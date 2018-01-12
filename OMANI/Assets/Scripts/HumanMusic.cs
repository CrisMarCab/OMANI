using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMusic : MonoBehaviour {

    public GameObject[] Close_Barroboys;
    float scale = 10;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        RaycastHit hit;

        float distanceToObstacle = 0;

        if (Physics.SphereCast(this.gameObject.transform.position, 10, transform.forward, out hit, 10))
        {
            if (hit.transform.tag=="persona") { 

            distanceToObstacle = hit.distance;

            }
        }

        BarroboyClip(distanceToObstacle);
    }

    void BarroboyClip(float distance) {

        //Hay que ver que valor máximo de audio queremos para hacer la escala.
        distance = Mathf.Clamp(distance, 1, scale);

        distance = -(distance - scale) / scale;
        //GameObject.FindObjectOfType<inteligencia_escena>().PlayWithVolume("HumanMusic", distance);

    }
}
