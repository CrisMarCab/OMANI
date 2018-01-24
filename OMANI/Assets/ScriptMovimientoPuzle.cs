using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptMovimientoPuzle : MonoBehaviour {

    public bool go = false;
    public bool up = true;
    [SerializeField] bool vertical = true;
    float velocity = 5f;

    void Update()
    {
        
            if (go)
            {
                if (vertical)
                {
                    if (up)
                    {
                
                        transform.localPosition = new Vector3(transform.localPosition.x + Time.deltaTime * velocity, transform.localPosition.y, transform.localPosition.z );
                    }
                    else
                    {

                        transform.localPosition = new Vector3(transform.localPosition.x - Time.deltaTime * velocity, transform.localPosition.y, this.transform.localPosition.z );

                    }
                }
                else
                {
                    if (up)
                    {

                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + Time.deltaTime * velocity);
                    }
                    else
                    {

                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, this.transform.localPosition.z - Time.deltaTime * velocity);

                    }

                }
            }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "IncubadoraNacimiento")
        {
            if (go == true)
            {
                go = false;
            }
            
        }
        if (collision.transform.tag == "Player" || collision.transform.tag == "persona")
        {
            collision.transform.parent = this.transform;
        }
       
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "persona")
        {
            collision.transform.parent = null;
        }
    }

}
