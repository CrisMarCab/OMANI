using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementosPasillo : MonoBehaviour
{

    public GameObject[] elementos_pasillo;
    public bool lightreduction = false;
    VolumetricLight Luz;
    float Coef;
    // Use this for initialization
    void Start()
    {
        Luz = elementos_pasillo[1].GetComponent<VolumetricLight>();

    }

    // Update is called once per frame
    void Update()
    {
        if (lightreduction == true)
        {
            Coef = Mathf.Lerp(Luz.ScatteringCoef, 0, 0.15f);
            Luz.ScatteringCoef = Coef;
        }

    }

    public IEnumerator DesactivarPasillo()
    {
        yield return new WaitForSeconds(1.5f);

        foreach (GameObject elemento in elementos_pasillo)
        {
            if (elemento!=null) {
                elemento.SetActive(false);
            }
        }

        elementos_pasillo[5].SetActive(true);

    }
}