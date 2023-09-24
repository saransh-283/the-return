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
    public TextMeshProUGUI descriptionText;

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

    public IEnumerator LocationChangeFadeOutIn(LocationChangeDelegate changeLocation, GameController controller)
    {
        float duration = 0.5f;
        float elapsedTime = 0f;

        AudioSource backgroundMusic = controller.player.currentLocation.GetComponent<AudioSource>();
        AudioClip transitionSound = controller.player.connection?.transitionSound;

        Color opaqueBlack = Color.black;
        Color transparentBlack = new Color(opaqueBlack.r,opaqueBlack.g,opaqueBlack.b,0);
        Color opaqueWhite = Color.white;
        Color transparentWhite = new Color(opaqueWhite.r, opaqueWhite.g, opaqueWhite.b, 0);

        string description = controller.introText;

        SoundManager.StopAllAudio();

        // Play transition sound
        if(transitionSound != null)
        {
            transitionSource.clip = transitionSound;
            transitionSource.Play();
        }

        // Black Screen Fade In
        if (!blackScreen.enabled)
        {
            description = controller.player.currentLocation.description;
            blackScreen.enabled = true;
            while (elapsedTime < duration)
            {
                Color tempColor = Color.Lerp(transparentBlack, opaqueBlack, elapsedTime / duration);
                blackScreen.color = tempColor;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        
        changeLocation();

        // Display Description Fade In
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            descriptionText.text = description;
            Color tempColor = Color.Lerp(transparentWhite, opaqueWhite, elapsedTime / duration);
            descriptionText.color = tempColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        // Display Description Fade Out
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            Color tempColor = Color.Lerp(opaqueWhite, transparentWhite, elapsedTime / duration);
            descriptionText.color = tempColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        descriptionText.text = "";
        // Black Screen Fade Out
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            Color tempColor = Color.Lerp(opaqueBlack, transparentBlack, elapsedTime / duration);
            blackScreen.color = tempColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SoundManager.StopAllAudio();
        backgroundMusic.Play();
        blackScreen.enabled = false;
    }
}
