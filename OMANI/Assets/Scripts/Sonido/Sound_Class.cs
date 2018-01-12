using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound_Class
{

    [Header("Sonido")]
    [Space]

    public string name;
    public AudioClip clip;

    public  Transform position;

    [HideInInspector]
    public AudioSource source;

    public bool bypassEffects, bypassListenerEffects, bypassReverbZones, playOnAwake, loop;
    [Space]

    [Header("Ajustes")]
    [Range(0f, 256f)]
    public int Priority;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    [Space]

    [Header("3D")]
    [Range(-1f, 1f)]
    public float stereoPan;
    [Range(0f, 1f)]
    public float spatialBlend;
    [Range(0f, 1.1f)]
    public float reverbZoneMix;
    [Range(0f, 5f)]
    public float dopplerLevel;
    [Range(0f, 360f)]
    public float Spread;

    [Space]
    [Header("Tamaño sonidos")]
    public float MinDistance;
    public float MaxDistance;
    
    
}
