using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{ 
    public static IEnumerator StopAllAudio()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
        }

        yield return null;
    }

    public static IEnumerator PlaySound(AudioClip clip, AudioSource audioSource)
    {
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(5);
        audioSource.Stop();
    }
}
