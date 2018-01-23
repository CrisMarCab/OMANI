using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;

public class camara_MindControl : MonoBehaviour
{
    public GameObject controled;
    float dist, speed = 5;

    Vector2 mouseLook;
    Vector2 smoothV;

    GameObject objeto,palanca;
    Transform ObjectPosition;

    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    //eventos para click izquierdo
    public delegate void LeftMouse(GameObject active, Npc_stats stats, Vector3 Camforward);
    public static event LeftMouse leftClick;
    Camera camara;

    public delegate void DesactivarAcciones();
    public static event DesactivarAcciones Desactivar;
    public  CinemachineVirtualCamera CamaraVirtual_POV,CamaraVirtual_POV_Palancas;
    CinemachinePOV POV;
    bool gotThere = false, lever;
    ParticleLink particles;
    // Use this for initialization
    void Start()
    {
        camara = Camera.main;
       
        particles = GameObject.FindObjectOfType<ParticleLink>().GetComponent<ParticleLink>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controled != null)
        {
            GameObject cabeza = FindTransform(controled.transform, "CameraPosition").gameObject;
            ObjectPosition = cabeza.transform.Find("ObjectPosition").transform;
            if (!gotThere)
            {

                lever = controled.GetComponent<Npc_stats>().lever;

                if (lever)
                {
                    controled.GetComponent<Npc_stats>().obj_carry = null; //le quito el objeto si llevaba uno
                    objeto = controled.GetComponentInChildren<AIRig>().AI.WorkingMemory.GetItem<GameObject>("objective"); //el objeto es el interruptor padre
                    palanca = objeto.transform.Find("lever").gameObject; //la palanca

                    //activo la camara para interruptores y le configuro los parametros
                    CamaraVirtual_POV_Palancas.transform.gameObject.SetActive(true);
                    CamaraVirtual_POV_Palancas.Follow = cabeza.transform;
                    CamaraVirtual_POV_Palancas.LookAt = objeto.transform.Find("CameraFocus").transform;
                    POV = CamaraVirtual_POV_Palancas.GetCinemachineComponent<CinemachinePOV>();

                }
                else
                {
                    //activo la camara POV libre  y le configuro los parametros
                    CamaraVirtual_POV.transform.gameObject.SetActive(true);
                    CamaraVirtual_POV.Follow = cabeza.transform;
                    POV = CamaraVirtual_POV.GetCinemachineComponent<CinemachinePOV>();

                }


                if (Vector3.Distance(camara.transform.position, cabeza.transform.position) < 0.2f)
                {

                    Cursor.lockState = CursorLockMode.Locked; //hacer desaparecer el cursor

                    if (controled.GetComponent<Npc_stats>().obj_carry != null)
                    {
                        objeto = controled.GetComponent<Npc_stats>().obj_carry; //cojo el objeto que esta llevando, si es que tiene
                    }

                   
                    gotThere = true; //Activo los controles de raton (left y right click)
                    Debug.Log(lever);
                }
            }
            else
            {
                if (!lever)//NORMAL 
                {
                    //POV.transform.position = controled.transform.forward;
                    
                    
                    if (objeto != null) //Poner el objeto en su posición
                    {
                        objeto.transform.position = ObjectPosition.position;
                        objeto.transform.rotation = ObjectPosition.rotation;
                    }
                    
                    
                    controled.transform.rotation = Quaternion.AngleAxis(POV.m_HorizontalAxis.Value , controled.transform.up);

                    //NOW WE CONTROL THE CLICKS!!
                    //---------------------------------------------------------------


                    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        
                        if (Input.GetMouseButtonDown(1))
                        {
                            if (hit.transform.tag == "persona")
                            {
                                
                                gotThere = false;
                                controled = hit.transform.gameObject;
                                controled.GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem("state", "idle");

                            }
                            else
                            {

                                cabeza = null;//Esto esta aqui porque es variable local (solo en el update)
                                goBacktoNormal();
                            }


                        }
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        objeto = null;
                        leftClick(controled, controled.GetComponent<Npc_stats>(), camara.transform.forward);
                    }
                   // controled.transform.forward = camara.transform.forward;
                }
                else //INTERRUPTOR / PALANCA
                {
                    

                    
                    float smoother = 100f;
                    var z = Input.GetAxisRaw("Mouse X");
                    var x = Input.GetAxisRaw("Mouse Y");
                    if (z > 0) {
                        z = z/smoother;
                    } else if (z < 0)
                    {
                        z = z/ smoother;
                    }

                    if (x > 0)
                    {
                        x = x/smoother;
                    }
                    else if (x < 0)
                    {
                        x = x / smoother;
                    }

                    float xFinal, zFinal;
                    leverOptions options = objeto.GetComponent<leverOptions>();
                    if ((palanca.transform.localPosition.z > -0.1 && palanca.transform.localPosition.z < 0.1) && options.centro || (palanca.transform.localPosition.z < -0.8 && options.izquierda ) || (palanca.transform.localPosition.z > 0.8 && options.derecha))
                    {
                        xFinal = Mathf.Clamp(palanca.transform.localPosition.x - x, -0.25f, 0.25f);
                    } else
                    {
                        xFinal = Mathf.Clamp(palanca.transform.localPosition.x - x , -0.03f, 0.03f);

                    }

                    if (palanca.transform.localPosition.x > -0.1 && palanca.transform.localPosition.x < 0.1 || palanca.transform.localPosition.x < -0.8 || palanca.transform.localPosition.x > 0.8)
                    {
                        zFinal = Mathf.Clamp(palanca.transform.localPosition.z + z, -1, 1);
                    }
                    else
                    {
                        //Limita el vertical a ciertas posiciones ()
                        if (palanca.transform.localPosition.z > 0.8)
                        {
                            zFinal = Mathf.Clamp(palanca.transform.localPosition.z+z, 0.9f, 1);
                        } else if (palanca.transform.localPosition.z < -0.8)
                        {
                            zFinal = Mathf.Clamp(palanca.transform.localPosition.z+z, -0.9f, -1f);
                        } else
                        {

                            zFinal = Mathf.Clamp(palanca.transform.localPosition.z+z, -0.05f, 0.05f);
                        }
                        
                       // zFinal = Mathf.Clamp(palanca.transform.localPosition.z, -0.1f, 0.1f);
                    }

                    palanca.transform.localPosition = new Vector3(xFinal, palanca.transform.localPosition.y, zFinal);







                    if (Input.GetMouseButtonDown(1))
                    {
                        //Tiene ciertas cosas especificas que lo diferencian de el click normal, pero se usa lo mismo despues (gobacktoNormal())
                        
                        cabeza = null;//Esto esta aqui porque es variable local (solo en el update)
                            goBacktoNormal();
                    }
                }
            }
        }
    }

    public void goBacktoNormal()
    {

        controled.GetComponent<Npc_stats>().lever = false;
        controled.GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem("state", "free");

        objeto = null;
        palanca = null; //reinicio variables
        
        controled = null;
        gotThere = false;

        /*
        if (Desactivar != null)
        {
            Desactivar(); //esto desactiva todas las acciones (de momento solo la mano)
        }
        */
        
        particles.liberar(); //Free particles

        Lenguaje scriptlenguaje = GameObject.FindObjectOfType<Lenguaje>();

        scriptlenguaje.enabled = true; //enable main controls

       
        
        Cursor.lockState = CursorLockMode.None;



        //diable this script

        CamaraVirtual_POV.transform.gameObject.SetActive(false);
        CamaraVirtual_POV_Palancas.transform.gameObject.SetActive(false);
        this.enabled = false;

    }
    public static Transform FindTransform(Transform parent, string name)
    {
        if (parent.name.Equals(name)) return parent;
        foreach (Transform child in parent)
        {
            Transform result = FindTransform(child, name);
            if (result != null) return result;
        }
        return null;
    }

}
