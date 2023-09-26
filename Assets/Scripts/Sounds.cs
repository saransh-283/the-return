using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound
{
    public AudioClip clip;
    public float volume;
    public float stereoPan;
    public Sound(AudioClip _audioClip, float _volume = 0.4f, float _steroPan = 0)
    {
        this.clip = _audioClip;
        this.volume = _volume;
        this.stereoPan = _steroPan;
    }
}
