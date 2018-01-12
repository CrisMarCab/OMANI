using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2_Inicio_Motor : MonoBehaviour {

    public GameObject boton1, boton2;
    private Animation[] animations;
    private MeshRenderer[] cables;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="objeto") {
            boton1.GetComponent<S2_Inicio_Botones>().enabled = true;
            boton2.GetComponent<S2_Inicio_Botones>().enabled = true;

            animations = GetComponentsInChildren<Animation>();

            foreach (Animation anim in animations) {
                anim.Play();
            }

            cables = GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer cabl in cables)
            {
                foreach (Material matt in cabl.materials) { 
                    if (matt.name == "armario Negro (Instance)")
                    {

                        matt.EnableKeyword("_EMISSION");

                        matt.color = Color.yellow;

                        Color baseColor = Color.yellow; //Replace this with whatever you want for your base color at emission level '1'

                        Color finalColor = baseColor * Mathf.LinearToGammaSpace(1);

                        matt.SetColor("_EmissionColor", finalColor);

                    }
                }
            }
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<AudioSource>().Play();
        }
        
    }
}
