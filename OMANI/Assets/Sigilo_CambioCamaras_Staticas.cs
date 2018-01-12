using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sigilo_CambioCamaras_Staticas : MonoBehaviour
{

    [SerializeField]
    private Transform camaraZona;
    private bool followingPlayer;
    public GameObject[] zoneCameras;

    private Transform target;
    private Camera mainCamera;
    private ControlCamara controlCamara;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = Camera.main;
        controlCamara = FindObjectOfType<ControlCamara>();

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!followingPlayer)
        {
            Vector3 relativePos = target.transform.position - camaraZona.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            camaraZona.transform.rotation = Quaternion.Lerp(camaraZona.transform.rotation, rotation, Time.deltaTime * 0.6f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            followingPlayer = true;

            mainCamera.transform.position = camaraZona.transform.position;
            mainCamera.transform.rotation = camaraZona.transform.rotation;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            followingPlayer = false;
        }
    }


}


