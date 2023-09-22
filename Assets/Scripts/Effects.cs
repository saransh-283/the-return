using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Effects : MonoBehaviour
{
    public Image blackScreen;
    public AudioSource transitionSource;
    public AudioSource effectsSource;

    public delegate void LocationChangeDelegate();


    private void Start()
    {
        blackScreen.enabled = true;
    }
    public IEnumerator FadeOutAndReset(TMP_InputField inputField)
    {
        float duration =.3f;
        Color startColor = inputField.textComponent.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Color tempColor = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            inputField.textComponent.color = tempColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        inputField.textComponent.color = targetColor;
        inputField.text = "";
        inputField.textComponent.color = startColor;
    }

    public IEnumerator LocationChangeFadeOutIn(LocationChangeDelegate changeLocation, AudioSource backgroundMusic, AudioClip transitionSound = null)
    {
        float duration = 1f;
        float elapsedTime = 0f;

        Color opaque = Color.black;
        Color transparent = new Color(opaque.r,opaque.g,opaque.b,0);

        SoundManager.StopAllAudio();
        if(transitionSound != null)
        {
            transitionSource.clip = transitionSound;
            transitionSource.Play();
        }

        if (!blackScreen.enabled)
        {
            blackScreen.enabled = true;
            while (elapsedTime < duration)
            {
                Color tempColor = Color.Lerp(transparent, opaque, elapsedTime / duration);
                blackScreen.color = tempColor;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        
        changeLocation();

        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Color tempColor = Color.Lerp(opaque, transparent, elapsedTime / duration);
            blackScreen.color = tempColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SoundManager.StopAllAudio();
        backgroundMusic.Play();
        blackScreen.enabled = false;
    }
}
