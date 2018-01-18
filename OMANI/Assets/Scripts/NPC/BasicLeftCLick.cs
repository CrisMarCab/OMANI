using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLeftCLick : MonoBehaviour {
 
    float force = 20;
    GameObject mano;
    Animator movimiento;
    // Use this for initialization
    void Start () {
        camara_MindControl.leftClick += LeftClick;
       // camara_MindControl.Desactivar += desactivarMano;
    }
    private void Update()
    {
        if (mano != null) {
            movimiento = mano.GetComponent<Animator>();
            /*
            if (movimiento.GetCurrentAnimatorStateInfo(0).IsName("LeftBasicAction")) {
                //ESTO NO FUNCA
                mano.SetActive(true);
            }
            */
        }
    }

    void LeftClick(GameObject active, Npc_stats stats, Vector3 Camforward)
    {

        
        if (transform.gameObject == active)
        {

            if (stats.obj_carry != null)
            {

                Debug.Log("throw");
                Rigidbody rb = stats.obj_carry.GetComponent<Rigidbody>();

                rb.useGravity = true;
                rb.isKinematic = false;

                stats.obj_carry.GetComponent<Collider>().enabled = true;
                rb.AddForce(Camforward * force, ForceMode.Impulse);
                stats.obj_carry = null;



            }
            else {
                //hay que activar una animacion
                Debug.Log("Ataque!");
                var anim = GetComponent<Animator>();
                    anim.SetTrigger("Ataque");
                RaycastHit hit;

                Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 10);
                foreach (Collider hitted in hitColliders)
                {
                    if (hitted.transform.tag == "objeto")
                    {
                        stats.obj_carry = hitted.gameObject;
                        return;
                    }
                }
                //anim.ResetTrigger("Ataque");

            }
        }
    }
    
   
    void OnDestroy()
    {
        camara_MindControl.leftClick -= LeftClick;
    }
}
