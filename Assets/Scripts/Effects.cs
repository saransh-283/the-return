using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Effects : MonoBehaviour
{
    public Image blackScreen;
    public AudioSource backgroundMusicSource;
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
        controller.textEntryField.interactable = false;
        float duration = 0.5f;
        float elapsedTime = 0f;

        Location currentLocation = controller.player.currentLocation;
        AudioClip transitionSound = controller.player.connection?.transitionSound;

        Color opaqueBlack = Color.black;
        Color transparentBlack = new Color(opaqueBlack.r,opaqueBlack.g,opaqueBlack.b,0);
        Color opaqueWhite = Color.white;
        Color transparentWhite = new Color(opaqueWhite.r, opaqueWhite.g, opaqueWhite.b, 0);

        string description = controller.introText;
        StartCoroutine(SoundManager.StopAllAudio());

        // Play transition sound
        if (transitionSound != null && controller.player.currentLocation.locationName != "The End")
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

        StartCoroutine(SoundManager.StopAllAudio());

        backgroundMusicSource.clip = currentLocation.clip;
        backgroundMusicSource.volume = currentLocation.volume;
        backgroundMusicSource.panStereo = currentLocation.stereoPan;
        backgroundMusicSource.Play();

        blackScreen.enabled = false;
        controller.textEntryField.interactable = true;
        controller.textEntryField.ActivateInputField();
    }
}
