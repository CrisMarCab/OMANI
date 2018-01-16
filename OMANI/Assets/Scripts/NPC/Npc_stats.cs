using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RAIN.Core;
using RAIN.Action;
using RAIN.Entities;
using RAIN.Entities.Aspects;

public class Npc_stats : MonoBehaviour
{
    public int  currentRdirection, currentOdirection, xmanos, ymanos, zmanos;
    public GameObject obj_carry = null, hands, player;
    private NavMeshAgent agent;
    private Animator anim;
    private float randomGenerator;
    private AIRig aiRig = null;
    private RaycastHit hittedground;
    public string namePlusId;
    public bool lever;
    public Vector3 IKpalanca;

    private int id;
    private Transform grabPosition;

    // Musica
    [SerializeField]
    public float distance;
    private AudioSource sonido_base;
    private int layerMask = 1 << 15;
    public GameObject npcMasCercano = null;
    public RaycastHit[] npcCercanos;

    // Use this for initialization
    void Start()
    {
        id = Random.Range(0,50000);

        currentRdirection = 0;
        player = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        //EVENTOS
        Lenguaje.follow += Follow;
        Lenguaje.pickup += Pickup;
        Lenguaje.lever += Lever;
        Lenguaje.attack += Attack;

        aiRig = gameObject.GetComponentInChildren<AIRig>();
        sonido_base = GetComponent<AudioSource>();

        //STRING PARA PONER A LOS OBJETIVOS QUE DETECTAR CON VISTA/SONIDO
        namePlusId = transform.gameObject.name + id;
        aiRig.AI.WorkingMemory.SetItem("objectiveString", namePlusId);

    }
    void FixedUpdate()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);

        npcCercanos = Physics.SphereCastAll(this.transform.position, 35f, Vector3.forward, 35f, layerMask);

        foreach (RaycastHit npc in npcCercanos)
        {
            Npc_stats npcstats = npc.transform.gameObject.GetComponent<Npc_stats>();
            if (npc.transform.tag == "persona" && npcstats != null)
            {
                if (distance < npcstats.distance)
                {
                    npcMasCercano = this.gameObject;
                }
                else if (distance > npcstats.distance)
                {
                    npcMasCercano = npc.transform.gameObject;
                }
            }
        }

        if (sonido_base != null)
        {

            if (this.gameObject == npcMasCercano)
            {
                sonido_base.volume = 1;
            }
            else
            {
                sonido_base.volume = 0;

            }
        }

        if (obj_carry != null)
        {
            //Player
            if (obj_carry.tag == "Player" || obj_carry.tag == "persona")
            {
                Vector3 relativePos = this.transform.position - obj_carry.transform.position;

                obj_carry.transform.GetChild(0).GetChild(0).gameObject.transform.position = Vector3.Lerp(obj_carry.transform.GetChild(0).GetChild(0).gameObject.transform.position, new Vector3(hands.transform.position.x, hands.transform.position.y, hands.transform.position.z), 9f * Time.deltaTime);
                //Quaternion rotation = Quaternion.LookRotation(new Vector3(relativePos.x , relativePos.y , relativePos.z));
                //obj_carry.transform.rotation = Quaternion.Slerp(obj_carry.transform.rotation, rotation, 2f * Time.deltaTime);
                if (obj_carry.tag == "Player")
                    FindObjectOfType<ControlCamara>().PlayerPosition = this.gameObject;

            }
            //Objects
            else
            {
                obj_carry.transform.position = Vector3.Lerp(obj_carry.transform.position, new Vector3(hands.transform.position.x, hands.transform.position.y, hands.transform.position.z), 5 * Time.deltaTime);
            }
        }
        else {
            if (anim.GetBool("PickingUp"))
            {
                anim.SetBool("PickingUp", false);//Stop the pickupAnimation
            }
        }

        // NPC Falling off 
        if (!Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), -Vector3.up, 100.0f))
        {
            StartCoroutine(this.gameObject.transform.GetComponentInChildren<Ragdoll>().ragdollTrueNPC(0.05f));
        }

        if (lever) { //Cuando has llegado al LEVER lo movemos a la posicion que interesa y lo preparamos para 
            var actualLever = aiRig.AI.WorkingMemory.GetItem<GameObject>("objective");
            GameObject position = actualLever.transform.Find("position").gameObject;
            IKpalanca = actualLever.transform.Find("lever").transform.position;
            transform.position = Vector3.Lerp(transform.position, position.transform.position,Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, position.transform.rotation, Time.deltaTime);
        }

    }

    void Pickup(GameObject active, GameObject w1)
    {
        //changes variables on Ai so that i goes on to pickup state
        if (transform.gameObject == active)
        {

            w1.AddComponent<EntityControler>();
            EntityControler controlerScript = w1.AddComponent<EntityControler>();
            controlerScript.VeryImportantPerson = this.transform.gameObject;

            aiRig.AI.WorkingMemory.SetItem("objective", w1);
            aiRig.AI.WorkingMemory.SetItem("state", "pickup");
        }
    }
    void Attack(GameObject active, GameObject w1)
    {
        //changes variables on Ai so that i goes on to pickup state
        if (transform.gameObject == active)
        {

            w1.AddComponent<EntityControler>();
            EntityControler controlerScript = w1.AddComponent<EntityControler>();
            controlerScript.VeryImportantPerson = this.transform.gameObject;

            aiRig.AI.WorkingMemory.SetItem("objective", w1);
            aiRig.AI.WorkingMemory.SetItem("state", "attack");
        }
    }
    void Follow(GameObject active, GameObject w1)
    {
        if (transform.gameObject == active)
        {
            GameObject objFollow = new GameObject();
            objFollow.transform.position = w1.transform.position;
            EntityControler controlerScript = objFollow.AddComponent<EntityControler>();
            controlerScript.VeryImportantPerson = this.transform.gameObject;

            //change state to follow, and gives objective
            aiRig.AI.WorkingMemory.SetItem("objective", objFollow);
            aiRig.AI.WorkingMemory.SetItem("state", "follow");
        }
    }
   
    void Lever(GameObject active,GameObject plever)
    {
        if (transform.gameObject == active)
        {
            EntityControler controlerScript = plever.AddComponent<EntityControler>();
            controlerScript.VeryImportantPerson = this.transform.gameObject;

            //change state to follow, and gives objective
            aiRig.AI.WorkingMemory.SetItem("objective", plever);
            aiRig.AI.WorkingMemory.SetItem("state", "lever");
        }
    }

   

    //Used by the animation events to play the steps.
    void rightStep()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hittedground))
        {
            if (hittedground.collider.gameObject.tag == "Cemento")
            {
                //FindObjectOfType<inteligencia_escena>().PlayAtPosition("Paso Cemento Derecho", hittedground.transform);
            }

            else if (hittedground.collider.gameObject.tag == "Cesped")
            {
                //FindObjectOfType<inteligencia_escena>().PlayAtPosition("Paso Cesped 1", hittedground.transform);

            }
        }
    }
    void leftStep()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hittedground))
        {
            if (hittedground.collider.gameObject.tag == "Cemento")
            {
                //FindObjectOfType<inteligencia_escena>().PlayAtPosition("Paso Cemento Izquierdo", hittedground.transform);
            }
            else if (hittedground.collider.gameObject.tag == "Cesped")
            {
                //FindObjectOfType<inteligencia_escena>().PlayAtPosition("Paso Cesped 2", hittedground.transform);
            }
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.tag == "persona" || collision.gameObject.tag == "Player")
            {
                if (this.gameObject.transform.InverseTransformPoint(contact.otherCollider.gameObject.transform.position).x > 0)
                {
                    anim.SetBool("ColisionLeft", true);
                }
                else if (this.gameObject.transform.InverseTransformPoint(contact.otherCollider.gameObject.transform.position).x < 0)
                {
                    anim.SetBool("ColisionRight", true);

                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "persona" || collision.gameObject.tag == "Player")
        {
            anim.SetBool("ColisionLeft", false);

            anim.SetBool("ColisionRight", false);

        }
    }
    void OnDestroy()
    {
        Lenguaje.follow -= Follow;
        Lenguaje.pickup -= Pickup;
        Lenguaje.lever -= Lever;
    }
    
    private void OnAnimatorIK()
    {
        //aiRig.AI.WorkingMemory.GetItem<GameObject>("objective").transform.Find("level").transform.position Esto es lo que dice mi cabessa
        if (lever) {
            anim.SetIKPosition(AvatarIKGoal.RightHand, IKpalanca); 
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, IKpalanca);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        }
        if (obj_carry!=null)
        {
            anim.SetIKPosition(AvatarIKGoal.RightHand, obj_carry.transform.position);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.2f);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, obj_carry.transform.position);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.2f);
        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        }

    }
    

}
