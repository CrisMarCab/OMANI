using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;

public class ParticleLink : MonoBehaviour
{
    //parte 1 siempre es el primer click, el 2 el segundo y el tercero el botón derecho.
    public GameObject parte1, parte2, selected = null;
    private string situacion;
    public GameObject greeneffect, redeffect, blueeffect;
    private GameObject destination;
    private Vector3 mouse;
    public RuntimeAnimatorController Controledanimatons;
    private RuntimeAnimatorController  PreviousControler;
    // Use this for initialization


    GameObject lasttouchedobject = null;
    cakeslice.Outline OutlineLastObject = null;


    // Update is called once per frame

    void Start()
    {
        //Controledanimatons = Resources.Load("Assets/Prefabs Genericos/Animaciones/NPC_BASIC/ControledAnims") as RuntimeAnimatorController; Esto estaria bien; pero no me funca
        destination = new GameObject();
        StartCoroutine(crearLink());
    }
    private void Update()
    {

        RaycastHit vHit = new RaycastHit();
        Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        #region Sistema de Outline cuando ratón encima
        if (Physics.Raycast(vRay, out vHit, 1000))
        {
            mouse = new Vector3(vHit.point.x, vHit.point.y + 1, vHit.point.z); ;
            destination.transform.position = mouse;
            if (vHit.transform.tag == "objeto" || vHit.transform.tag == "lever")
            {
                lasttouchedobject = vHit.transform.gameObject;

                OutlineLastObject = lasttouchedobject.transform.gameObject.GetComponent<cakeslice.Outline>();
                if (OutlineLastObject != null)
                {
                    OutlineLastObject.eraseRenderer = false;

                }

            }
            else if (vHit.transform.tag == "persona")
            {

                lasttouchedobject = vHit.transform.gameObject;
                OutlineLastObject = lasttouchedobject.GetComponentInChildren<cakeslice.Outline>();
                if (OutlineLastObject != null)
                {
                    OutlineLastObject.eraseRenderer = false;
                }
            }


            else
            {
                if (OutlineLastObject != null )
                {
                    if (selected != null) {
                        if (OutlineLastObject != selected.GetComponentInChildren<cakeslice.Outline>()) {

                            OutlineLastObject.eraseRenderer = true;
                        }
                    }else
                    {
                        OutlineLastObject.eraseRenderer = true;
                    }


                }
            }
            #endregion
        }
        if (selected != null)
        {
            selected.GetComponentInChildren<cakeslice.Outline>().eraseRenderer = false;

            //esto es para poner el modo "borracho/controlado" ne las animaciones
            AnimatorChange(selected);
        }
    }

    public void AnimatorChange(GameObject pSelected)
    {
        if (pSelected.transform.tag == "persona")
        {
            if (pSelected.GetComponent<Animator>().runtimeAnimatorController != Controledanimatons)
            {
                PreviousControler = pSelected.GetComponent<Animator>().runtimeAnimatorController;
                pSelected.GetComponent<Animator>().runtimeAnimatorController = Controledanimatons;
            }
        }
    }

    IEnumerator crearLink()
        {

        
        switch (situacion)
        {
            case "orden":
                if (parte1 != null)
                {

                    if (parte2 != null)
                    {
                        //particula de 1 a 2;
                        if (parte1.transform.tag == "objeto")
                        {
                            GameObject effect;
                            effect = Instantiate(blueeffect, parte1.transform.position, Quaternion.identity);
                            effect.GetComponent<LifeofaParticle>().destination = parte2.transform.gameObject;
                        }
                        else if (parte1.transform.tag == "persona")
                        {
                            GameObject effect;
                            effect = Instantiate(greeneffect, parte1.transform.position, Quaternion.identity);
                            effect.GetComponent<LifeofaParticle>().destination = parte2.transform.gameObject;
                        }
                        else if (parte1.transform.tag == "lever")
                        {
                            GameObject effect;
                            effect = Instantiate(redeffect, parte1.transform.position, Quaternion.identity);
                            effect.GetComponent<LifeofaParticle>().destination = parte2.transform.gameObject;

                        }
                        //particula de 2
                        if (parte2.transform.tag == "objeto")
                        {
                            GameObject effect;
                            effect = Instantiate(blueeffect, parte2.transform.position, Quaternion.identity);
                            effect.GetComponent<LifeofaParticle>().destination = parte1.transform.gameObject;
                        }
                        else if (parte2.transform.tag == "persona")
                        {
                            GameObject effect;
                            effect = Instantiate(greeneffect, parte2.transform.position, Quaternion.identity);
                            effect.GetComponent<LifeofaParticle>().destination = parte1.transform.gameObject;
                        }
                        else if (parte2.transform.tag == "lever")
                        {
                            GameObject effect;
                            effect = Instantiate(redeffect, parte2.transform.position, Quaternion.identity);
                            effect.GetComponent<LifeofaParticle>().destination = parte1.transform.gameObject;

                        }
                    }
                    else
                    {
                        //palabra 1 definida (se instancia el effecto y se le da de destino el ratón)
                       
                        if (parte1.transform.tag == "persona")
                        {
                            GameObject effect;
                            effect = Instantiate(greeneffect, parte1.transform.position, Quaternion.identity);
                            effect.GetComponent<LifeofaParticle>().destination = destination;
                        }
                        
                    }
                    
                }
                break;
            case "accion":

                break;
            default:
                break;
        }



        yield return new WaitForSeconds(0.1f);
        StartCoroutine(crearLink());
    }


    //pseudo constructores
    public void liberar()
    {
        Setselected(null);
        parte1 = null;
        parte2 = null;
        situacion = null;
    }
    public void cosaRaton(GameObject pparte1, string psituacion)
    {
        parte1 = pparte1;
        parte2 = null;
        situacion = psituacion;
    }
    public void dosCosas(GameObject pparte1, GameObject pparte2, string psituacion)
    {
        parte1 = pparte1;
        parte2 = pparte2;
        situacion = psituacion;
    }

    public void Setselected(GameObject pSelected)
    {
        if (selected != null)
        {
            if (!selected.GetComponent<Npc_stats>().lever && selected.GetComponentInChildren<AIRig>().AI.WorkingMemory.GetItem("state") == "free") { 
            selected.GetComponent<Animator>().runtimeAnimatorController = PreviousControler;
            }
            selected.transform.gameObject.GetComponentInChildren<cakeslice.Outline>().eraseRenderer = true;
            //aqui a parte de quitarle el outline al seleccionado, tambien le pongo el controlador de animaciones como estaba
        }
        selected = pSelected;

    }
}
