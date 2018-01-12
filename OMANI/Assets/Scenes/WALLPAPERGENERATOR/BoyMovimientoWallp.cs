﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyMovimientoWallp : MonoBehaviour {

    #region Variables
    //Shouting Audio
    public AudioClip shoutingClip;

    //Smoothing variables for turning the character
    public float smooth = 15f;

    //Reference Variables
    private Animator anim;
    private Animation animation;
    private Rigidbody rigid;
    public Ragdoll ragdll;
    public Collider coll;
    //Vector3 that keeps track of the mouse position
    private Vector3 mousePosition;


    //Speed for the animation
    private float AnimSpeed;
    //Time for the animation blend
    private float t, t2;
    #endregion

    #region Ragdollvariables
    //Button that activates the ragdoll
    [SerializeField]
    bool ragdollbutton;

    [SerializeField]
    //Additional vectors for storing the pose the ragdoll ended up in.
    public Vector3 ragdolledHipPosition, ragdolledHeadPosition, ragdolledFeetPosition;

    //A helper variable to store the time when we transitioned from ragdolled to blendToAnim state
    float ragdollingEndTime = -100;

    //How long do we blend when transitioning from ragdolled to animated
    public float ragdollToMecanimBlendTime = 0.5f;
    float mecanimToGetUpTransitionTime = 0.05f;

    [SerializeField]
    private GameObject lookat;

    public bool ragdolled
    {
        get
        {
            return state != RagdollState.animated;
        }
        set
        {
            if (value == true)
            {
                if (state == RagdollState.animated)
                {
                    //Transition from animated to ragdolled
                    ragdll.ragdollTrue(); //allow the ragdoll RigidBodies to react to the environment
                    anim.enabled = false; //disable animation
                    state = RagdollState.ragdolled;
                }
            }
            else
            {
                if (state == RagdollState.ragdolled)
                {
                    ragdll.ragdollFalse();
                    ragdollingEndTime = Time.time; //store the 
                                                   //Remember some key positions
                    anim.enabled = true;



                    state = RagdollState.blendToAnim;

                    ragdolledFeetPosition = 0.5f * (anim.GetBoneTransform(HumanBodyBones.LeftFoot).position + anim.GetBoneTransform(HumanBodyBones.RightFoot).position);
                    ragdolledHeadPosition = anim.GetBoneTransform(HumanBodyBones.Head).position;
                    ragdolledHipPosition = anim.GetBoneTransform(HumanBodyBones.Hips).position;
                    Debug.Log("Sragdolled2");
                    //Initiate the get up animation
                    anim.SetBool("Getup", true);

                } //if (state==RagdollState.ragdolled)
            }   //if value==false	
        } //set
    }

    enum RagdollState
    {
        animated,    //Mecanim is fully in control
        ragdolled,   //Mecanim turned off, physics controls the ragdoll
        blendToAnim  //Mecanim in control, but LateUpdate() is used to partially blend in the last ragdolled pose
    }

    //The current state
    RagdollState state = RagdollState.animated;
    #endregion

    private void Awake()
    {
        SetInitialReferences();
        anim.SetLayerWeight(1, 1f); //TODO : Study.
    }

    // Sets all the references for the script
    void SetInitialReferences()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        animation = GetComponent<Animation>();
        coll = GetComponent<CapsuleCollider>();
        //Gets the component of the children of the main character, which is "ProtaInterior".
        /*
         * Ragdoll searches for the components in children in charge of the Ragdoll system 
         * and desactivates or activates them. 
         */

        ragdll = GetComponentInChildren<Ragdoll>();

    }

    private void Start()
    {
        if (ragdollbutton == false) {
            anim.enabled = true;
            ragdolled = false;
        }
    }

    void FixedUpdate()
    {
        // MOVEMENT
        // This stores the input in both vertical and horizontal axis. 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //This function controls the movement.
        MovementController(h, v);
    }


    // Called uppon each frame. 
    void Update()
    {

        #region Ragdoll

        //RAGDOLL
        // Ragdoll button.


        if (Input.GetKeyDown(KeyCode.Space))
        {
            ragdolled = false;
            coll.enabled = true;
            rigid.isKinematic = false;
        }
        if (Input.GetKeyDown("r"))
        {
            ragdolled = true;
            coll.enabled = false;
            rigid.isKinematic = true;
        }
        #endregion
        #region Shout

        //SHOUT
        //Shout button.
        bool shout = Input.GetKeyDown("space");
        anim.SetBool("Shouting", shout);
        #endregion
    }

    // LateUpdate is called after all Update functions have been called.
    private void LateUpdate()
    {
        #region Ragdollstand
        //Clear the get up animation controls so that we don't end up repeating the animations indefinitely
        anim.SetBool("Getup", false);

        if (state == RagdollState.blendToAnim)
        {
            {
                if (Time.time <= ragdollingEndTime + mecanimToGetUpTransitionTime)
                {
                    //If we are waiting for Mecanim to start playing the get up animations, update the root of the mecanim
                    //character to the best match with the ragdoll
                    Vector3 animatedToRagdolled = ragdolledHipPosition - anim.GetBoneTransform(HumanBodyBones.Hips).position;
                    Vector3 newRootPosition = transform.position + animatedToRagdolled;

                    //Now cast a ray from the computed position downwards and find the highest hit that does not belong to the character 
                    RaycastHit[] hits = Physics.RaycastAll(new Ray(newRootPosition, Vector3.down));
                    newRootPosition.y = 0;
                    foreach (RaycastHit hit2 in hits)
                    {
                        if (!hit2.transform.IsChildOf(transform))
                        {
                            newRootPosition.y = Mathf.Max(newRootPosition.y, hit2.point.y);
                        }
                    }
                    transform.position = newRootPosition;

                    //Get body orientation in ground plane for both the ragdolled pose and the animated get up pose
                    Vector3 ragdolledDirection = ragdolledHeadPosition - ragdolledFeetPosition;
                    ragdolledDirection.y = 0;

                    Vector3 meanFeetPosition = 0.5f * (anim.GetBoneTransform(HumanBodyBones.LeftFoot).position + anim.GetBoneTransform(HumanBodyBones.RightFoot).position);
                    Vector3 animatedDirection = anim.GetBoneTransform(HumanBodyBones.Head).position - meanFeetPosition;
                    animatedDirection.y = 0;

                    //Try to match the rotations. Note that we can only rotate around Y axis, as the animated characted must stay upright,
                    //hence setting the y components of the vectors to zero. 
                    transform.rotation *= Quaternion.FromToRotation(animatedDirection.normalized, ragdolledDirection.normalized);
                }
                //compute the ragdoll blend amount in the range 0...1
                float ragdollBlendAmount = 1.0f - (Time.time - ragdollingEndTime - mecanimToGetUpTransitionTime) / ragdollToMecanimBlendTime;
                ragdollBlendAmount = Mathf.Clamp01(ragdollBlendAmount);

                //In LateUpdate(), Mecanim has already updated the body pose according to the animations. 
                //To enable smooth transitioning from a ragdoll to animation, we lerp the position of the hips 
                //and slerp all the rotations towards the ones stored when ending the ragdolling

                foreach (BodyPart b in ragdll.bodyParts)
                {
                    if (b.transform != transform)
                    { //this if is to prevent us from modifying the root of the character, only the actual body parts
                      //position is only interpolated for the hips
                        if (b.transform == anim.GetBoneTransform(HumanBodyBones.Hips))
                            b.transform.position = Vector3.Lerp(b.transform.position, b.storedPosition, ragdollBlendAmount);
                        //rotation is interpolated for all body parts
                        b.transform.rotation = Quaternion.Slerp(b.transform.rotation, b.storedRotation, ragdollBlendAmount);
                    }
                }

                //if the ragdoll blend amount has decreased to zero, move to animated state
                if (ragdollBlendAmount == 0)
                {
                    state = RagdollState.animated;
                    return;
                }
            }

        }
        #endregion


        #region mouseposition
        //Sends a ray to where the mouse is pointing at.
        Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Saves the information of the hit.
        RaycastHit hit;
        if (Physics.Raycast(cursorRay, out hit))
        {
            //Player is not taken into account due to weird behaviours.
            if (hit.transform.tag != "Player")
            {
                //3f Offset so that the character doesn't look to the ground.
                mousePosition = hit.point + 3f * Vector3.up;
            }
        }
        #endregion
    }

    void OnAnimatorIK()
    {

        //LOOK AT MOUSE
        //This function tells the Inverse Kinematics where to look at and stablishes its parameters.
        //LookAtWeight
        /*
         Parameters in order : 
         Global weight(multiplier for all the others), bodyWeight, headWeight, eyesWeight and clampWeight(0 means the character is unrestained in motion).
         */

        anim.SetLookAtWeight(1f, 0.2f, 0.4f, 1f, 1f);

        //Position too look at.
        anim.SetLookAtPosition(lookat.transform.position);

    }

    //Function in charge of the Main Character movement. Sends commands to the animator and allows the character to rotate.
    void MovementController(float horizontal, float vertical)
    {
        // If the axis has any sort of input.
        if (horizontal != 0f || vertical != 0f)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("GetUp"))
            {

                AnimSpeed = Mathf.Lerp(0, 4.5f, t);
                t += 0.8f * Time.deltaTime;
                // Calls the Rotate function, which makes the rotation of the character look good.
                Rotate(horizontal, vertical);
                anim.SetFloat("AnimSpeed", AnimSpeed);
            }
        }
        // If the axis doesn't have any sort of input.
        else
        {
            t = 0.2f;
            AnimSpeed = Mathf.Lerp(AnimSpeed, 0, t);
            t2 -= 0.4f * Time.deltaTime;
            anim.SetFloat("AnimSpeed", AnimSpeed);
        }
    }

    // Function that makes the rotation of the character look good.
    /* This rotate function is called upon each frame. The rotation is smoothed.*/
    void Rotate(float horizontal, float vertical)
    {
        // Determines the new direction of the character
        Vector3 desiredDirection = new Vector3(horizontal, 0f, vertical);

        desiredDirection = Camera.main.transform.TransformDirection(desiredDirection);
        desiredDirection.y = 0.0f;

        // Calculates the rotation at which the character is directed.
        Quaternion desiredRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);

        // Smoothes the transition
        Quaternion smoothedRotation = Quaternion.Lerp(rigid.rotation, desiredRotation, smooth * Time.deltaTime);

        // Uses the rigidbody function  "MoveRotation" which sets the new rotation of the Rigidbody. 
        rigid.MoveRotation(smoothedRotation);
    }


}