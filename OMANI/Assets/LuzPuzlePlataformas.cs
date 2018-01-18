﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzPuzlePlataformas : MonoBehaviour {
    bool activado = false ;
    
    [SerializeField] LuzPuzlePlataformas puzle1,puzle2;
    [SerializeField] GameObject Efectos, lucesHangar;


    // Update is called once per frame
    void Update () {
		if (activado && puzle1.activado && puzle2.activado)
        {
            lucesHangar.SetActive(true);
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "objeto")
        {
            activado = true;
            Efectos.SetActive(true);
        }
    }
}
