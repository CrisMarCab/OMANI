using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2_Cascada : MonoBehaviour {

    [SerializeField]
    GameObject player, Brain;
    AudioSource[] bases;
    AudioSource basesource;
    float sound, distance, originalvolume = 1;
    bool sound_dinamic = true;

    // Use this for initialization
    void Start()
    {
        bases = FindObjectOfType<AudioManager>().GetComponents<AudioSource>();

        foreach (AudioSource b in bases)
        {
            if (b.clip.name == "Patio de juegos (Base)")
            {
                basesource = b;
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sound_dinamic)
        {
            sound = Vector3.Distance(player.transform.position, this.transform.position);
            if (sound < 48) {
                sound = -(sound - 48) / 48;
                basesource.volume = originalvolume - sound;
            }
        }
    }

}
