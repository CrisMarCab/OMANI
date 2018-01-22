using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nave2D : MonoBehaviour {

    [SerializeField] bool piloto = false, player = false;
    [SerializeField] AudioSource error,motor;
    [SerializeField] GameObject Camara2D;
    public float speed = 5;
    // Update is called once per frame
    void Update () {
		if (piloto && player)
        {
            var input = Input.GetAxisRaw("Vertical");
            input = input * speed;

            transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y+input * Time.deltaTime, transform.localPosition.z + speed * Time.deltaTime);
        }
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "persona") {
            piloto = true;
            collision.gameObject.SetActive(false);
        }
        if (collision.transform.tag == "Player")
        {
            if (piloto)
            {
                collision.gameObject.SetActive(false);
                Camara2D.SetActive(true);
                player = true;
                motor.Play();

            }else
            {
                error.Play();
            }
        }
    }
}
