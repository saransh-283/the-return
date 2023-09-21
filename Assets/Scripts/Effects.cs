using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Effects : MonoBehaviour
{
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
}
