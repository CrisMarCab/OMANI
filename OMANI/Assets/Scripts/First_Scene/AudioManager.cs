using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;
public class AudioManager : MonoBehaviour
{

    //Sounds used by the script
    public Sound_Class[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);


        //Inicializamos los audios
        soundEngine();
    }

    // TODO : No tener que añadir a mano cada sonido. 

    //Inicializa el audio
    public void soundEngine()
    {
        //AudioSource components. For my boy Jaime to edit easy peasy.
        foreach (Sound_Class s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.spatialBlend = s.spatialBlend;
            s.source.bypassEffects = s.bypassEffects;
            s.source.bypassListenerEffects = s.bypassListenerEffects;
            s.source.bypassReverbZones = s.bypassReverbZones;
            s.source.playOnAwake = s.playOnAwake;
            s.source.loop = s.loop;
            s.source.clip = s.clip;

            s.source.priority = s.Priority;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.panStereo = s.stereoPan;
            s.source.spatialBlend = s.spatialBlend;
            s.source.reverbZoneMix = s.reverbZoneMix;
            s.source.dopplerLevel = s.dopplerLevel;
            s.source.spread = s.Spread;
            s.source.minDistance = s.MinDistance;
            s.source.maxDistance = s.MaxDistance;
        }

    }

    //Plays the audio clips.
    public IEnumerator Play(string name, float time)
    {
        yield return new WaitForSeconds(time);
        Sound_Class s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }


    //Plays the audio clips.
    public void Play(string name)
    {
        Sound_Class s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    //Play at position
    public void PlayAtPosition(string name, Vector3 position)
    {

        GameObject TempAudio = new GameObject("TempAudio");
        TempAudio.transform.position = position;

        Sound_Class s = Array.Find(sounds, sound => sound.name == name);

        s.source = TempAudio.AddComponent<AudioSource>();

        s.source.spatialBlend = s.spatialBlend;
        s.source.bypassEffects = s.bypassEffects;
        s.source.bypassListenerEffects = s.bypassListenerEffects;
        s.source.bypassReverbZones = s.bypassReverbZones;
        s.source.playOnAwake = s.playOnAwake;
        s.source.loop = s.loop;
        s.source.clip = s.clip;

        s.source.priority = s.Priority;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.panStereo = s.stereoPan;
        s.source.spatialBlend = s.spatialBlend;
        s.source.reverbZoneMix = s.reverbZoneMix;
        s.source.dopplerLevel = s.dopplerLevel;
        s.source.spread = s.Spread;
        s.source.minDistance = s.MinDistance;
        s.source.maxDistance = s.MaxDistance;



        AudioClip otherclip = s.source.clip;
        s.source.Play();

        Destroy(TempAudio, s.clip.length);

    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

}