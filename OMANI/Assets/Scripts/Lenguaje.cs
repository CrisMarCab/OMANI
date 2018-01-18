using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;

public class Lenguaje : MonoBehaviour
{
    AudioSource[] audios;
	public GameObject selected,protagonist,padrecamara;
    public ParticleLink particles;
	private GameObject nonObjectDirection;
	public GameObject direction;

    public delegate void doComplexAction(GameObject active, GameObject w1,GameObject w2);
    public static event doComplexAction temporal;

    public delegate void doSimpleAction(GameObject active, GameObject w1);
    public static event doSimpleAction follow,pickup,lever,attack;


    public delegate void doAction(GameObject active);
    public static event doAction basicaction;

    void Start()
    {
		
		nonObjectDirection = new GameObject();
		//direction.AddComponent (Navigation);
        //audios = GetComponents<AudioSource>();
        //estado = canvas.GetComponent<Text>();
    }
    

    void Update()
    {
		
        #region RightClick
        if (Input.GetMouseButtonDown(1))
        {
            if (selected != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    switch (hit.transform.tag)
                    {
                        case "objeto":
                            //Coge el objeto
                            selected.GetComponent<Npc_stats>().lever = false;
                            direction = hit.transform.gameObject;
							particles.dosCosas(selected,direction,"orden");
							pickup(selected,direction);
                            break;
                        case "lever":
                            direction = hit.transform.gameObject;
                            particles.dosCosas(selected, direction, "orden");
                            lever(selected, direction);
                            break;
                        case "Guardia":
                            direction = hit.transform.gameObject;
                            particles.dosCosas(selected, direction, "orden");
                            attack(selected, direction);
                            break;
                        case "persona":
						//comprobamos si es la misma persona que hay seleccionada
						if (hit.transform.gameObject == selected){
							particles.liberar();
                                particles.Setselected(selected);
                                MindControl(hit.transform.gameObject);
						}else{
                                //seguir al que has clickado
                            selected.GetComponent<Npc_stats>().lever = false;
							particles.liberar();
							direction = hit.transform.gameObject;
							particles.dosCosas(selected,direction,"orden");
							follow(selected, direction);
						}
							
                            break;
                        default:
                            //Que se mueva al punto del mapa clickado;
                            selected.GetComponent<Npc_stats>().lever = false;
                            direction = nonObjectDirection;
							direction.transform.position = hit.point;
							particles.dosCosas(selected,direction, "orden");
							follow(selected,direction);

                            break;
                    }

                }
			}else {

				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, 100))
				{
					Debug.DrawRay(ray.origin, ray.direction * 5000, Color.red);
					switch (hit.transform.tag)
					{
					case "persona":

                            selected = hit.transform.gameObject;
                            Animator anim = selected.GetComponent<Animator>();
                            anim.SetBool("AnimacionEspecial", false);

                            particles.liberar();
                            particles.Setselected(selected);
                            MindControl(hit.transform.gameObject);
                            break;
					}

				}
			}
            
        }

        #endregion
        #region LeftClick
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                
                if (hit.transform.tag == "persona")//solo puedes seleccionar personas, así que es el caso que nos interesa
                {
					
                    selected = hit.transform.gameObject;
                    particles.Setselected(selected); 
                    particles.cosaRaton(selected, "orden");
                    Animator anim = selected.GetComponent<Animator>();
                    anim.SetBool("AnimacionEspecial",false);
                    
                }
                else
                {
                    QuitarSeleccion();
                }
            }
        }
        #endregion

		//control de la particulas
		
    }

    public void QuitarSeleccion()
    {
        particles.liberar();
        selected = null;
    }

    void MindControl(GameObject controlObj) {
        //particles.selected = null;
        particles.liberar(); // adios particulas


        if (protagonist!= null)
        {
            protagonist.GetComponent<Animator>().SetFloat("AnimSpeed", 0); //paro al prota, para que no siga caminando
            //protagonist.GetComponent<BoyMovimiento>().enabled = false;
        }
		camara_MindControl mindcontrol = padrecamara.GetComponent<camara_MindControl>();
		mindcontrol.enabled = true;
		mindcontrol.controled = controlObj;
        controlObj.GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem("state","idle");
        particles.AnimatorChange(controlObj);

        this.enabled = false;
	}
}
