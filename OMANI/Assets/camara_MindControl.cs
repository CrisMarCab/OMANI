using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool gotThere = false, lever;
    Quaternion originalRotation;
    ParticleLink particles;
    // Use this for initialization
    void Start()
    {
        camara = Camera.main;
       
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
            rb.freezeRotation = true;
        originalRotation = Camera.main.transform.localRotation;
        particles = GameObject.FindObjectOfType<ParticleLink>().GetComponent<ParticleLink>();

        if (Camera.main.gameObject.GetComponent<Collider>() != null)
        {
            Camera.main.gameObject.GetComponent<Collider>().enabled = false; //desactivar collider de la camara
        }


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
                if (Camera.main.gameObject.GetComponent<Collider>() != null)
                {
                    Camera.main.gameObject.GetComponent<Collider>().enabled = false; //desactivar collider de la camara
                }
                //Getting close movement, with fov modification
                camara.transform.position = Vector3.Lerp(Camera.main.transform.position, cabeza.transform.position, Time.deltaTime * 15);
                camara.fieldOfView += 1f;
                if (Vector3.Distance(camara.transform.position, cabeza.transform.position) < 0.2f)
                {

                    controled.GetComponentInChildren<cakeslice.Outline>().eraseRenderer = true; //elimino el outline de quien controlo

                    camara.transform.parent = cabeza.transform; //camara como hijo de la cabeza

                    camara.fieldOfView = 70; //reestablezco el fov

                    //controled.GetComponentInChildren<Renderer>().enabled = false; PROBANDO SIN QUITAR EL RENDERER
                    controled.GetComponentInChildren<Rigidbody>().isKinematic = true;

                    Cursor.lockState = CursorLockMode.Locked; //hacer desaparecer el cursor

                    if (controled.GetComponent<Npc_stats>().obj_carry != null)
                    {
                        objeto = controled.GetComponent<Npc_stats>().obj_carry; //cojo el objeto que esta llevando, si es que tiene
                    }

                    lever = controled.GetComponent<Npc_stats>().lever;
                    if (lever)
                    {
                        objeto = controled.GetComponentInChildren<AIRig>().AI.WorkingMemory.GetItem<GameObject>("objective");
                        palanca = objeto.transform.Find("lever").gameObject;
                      
                    }
                    gotThere = true; //Activo los controles de raton (left y right click)
                    Debug.Log(lever);
                }
            }
            else
            {
                if (!lever)//NORMAL 
                {

                    camara.transform.rotation = new Quaternion(0, 0, 0, 0);
                    camara.transform.position = new Vector3(0, 0, 0);
                    //control the cam with mouse;
                    if (objeto != null)
                    {

                        objeto.transform.position = ObjectPosition.position;
                        objeto.transform.rotation = ObjectPosition.rotation;
                    }

                    Camera.main.transform.position = cabeza.transform.position;

                    var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

                    md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

                    smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
                    smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
                    mouseLook += smoothV;

                    // Limits the vertical angle of the camera.

                    mouseLook.y = Mathf.Clamp(mouseLook.y, -55f, 40f);

                    camara.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
                    controled.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, controled.transform.transform.up);

                    //NOW WE CONTROL THE CLICKS!!
                    //---------------------------------------------------------------


                    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        /*Da problemas porque hace que le cambie la animacion a la people, hay que usar otra cosa
                        if (hit.transform.tag == "persona")
                        {
                            particles.selected = hit.transform.gameObject;//highlight on people :D
                        }
                        else if (hit.transform.tag == "objeto")
                        {
                            particles.selected = hit.transform.gameObject;//highlight on people :D


                        }
                        else
                        {
                            particles.selected = null;
                        }
                        */


                        if (Input.GetMouseButtonDown(1))
                        {
                            if (hit.transform.tag == "persona")
                            {

                                controled.GetComponentInChildren<Renderer>().enabled = true;
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
                }
                else //INTERRUPTOR / PALANCA
                {
                    camara.transform.rotation = cabeza.transform.rotation;
                    camara.transform.position = cabeza.transform.position;

                    
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
        objeto = null;
        palanca = null; //reinicio variables

        controled.GetComponent<Npc_stats>().lever = false;
        
        if (Desactivar != null)
        {
            Desactivar(); //esto desactiva todas las acciones (de momento solo la mano)
        }
        
        controled.GetComponentInChildren<Collider>().enabled = true;
        controled.GetComponentInChildren<Rigidbody>().isKinematic = false;
        controled.GetComponentInChildren<Renderer>().enabled = true;

        if (Camera.main.gameObject.GetComponent<Collider>() != null)
        {
            Camera.main.gameObject.GetComponent<Collider>().enabled = true; //activar collider de la camara
        }
        Camera.main.transform.parent = this.transform; //Camara to original parent
        particles.liberar(); //Free particles

        Lenguaje scriptlenguaje = GameObject.FindObjectOfType<Lenguaje>().GetComponent<Lenguaje>();

        scriptlenguaje.enabled = true; //enable main controls

        if (scriptlenguaje.protagonist != null)
        {
            scriptlenguaje.protagonist.GetComponent<BoyMovimiento>().enabled = true; //enable movement
        }

        transform.GetComponent<ControlCamara>().enabled = true;
        controled.GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem("state", "free");
        Cursor.lockState = CursorLockMode.None;


        //Restart local variables
        controled = null;
        gotThere = false;

        //diable this script
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
