using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem_Physics : MonoBehaviour
{
    // Use this for initialization

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        rb.AddExplosionForce(100, new Vector3(25.44f - 0.02f, 0.1400003f + 5.65f, -3.98f - 0.04410558f), 50f, 3.0f);

    }


}
