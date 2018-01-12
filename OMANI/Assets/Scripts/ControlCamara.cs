using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ControlCamara : MonoBehaviour
{
    [SerializeField]
    public Transform main, originalrotation;

    [SerializeField]
    public Transform[] states = new Transform[10];
    [SerializeField]
    public bool CameraWatchingPlayer, StaticCamera;

    [SerializeField]
    public GameObject PlayerPosition;
    public int state;

    bool movedcamera = false;

    [SerializeField]
    PostProcessingProfile fxProfile;


    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Use this for initialization
    void Start()
    {
        if (GetComponentInChildren<PostProcessingBehaviour>().profile != null)
        {
            fxProfile = GetComponentInChildren<PostProcessingBehaviour>().profile;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (CameraWatchingPlayer == true)
        {
            if (StaticCamera == true)
            {
                Vector3 relativePos = PlayerPosition.transform.position - mainCamera.transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, rotation, Time.deltaTime * 0.6f);
                if (fxProfile != null)
                {
                    fxProfile.grain.enabled = true;
                }
            }

            else
            {
                transform.position = Vector3.Lerp(transform.position, PlayerPosition.transform.position, Time.deltaTime * 0.9f);

                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, states[state].transform.position, Time.deltaTime * 0.8f);
                mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, states[state].transform.rotation, Time.deltaTime * 0.8f);

                if (fxProfile != null)
                {
                    fxProfile.grain.enabled = false;
                }
            }
        }


    }


    //Arreglar futuro
    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "changecamera1")
        {

            if (state == 1)
            {
                state = 2;
            }
            else
            {
                state = 1;
            }
        }
    }

}
