using System;
using UnityEngine;

[Serializable]
public class AudioData
{
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume = 1;
}