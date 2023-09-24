using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventSystem : MonoBehaviour
{
    public TMP_InputField nameInput;
    public Button playButton;

    private void Start()
    {
        // Initially, disable the button
        playButton.interactable = false;

        // Add an event listener to the Input Field's text change event
        nameInput.onValueChanged.AddListener(SetPlayerName);
    }
    public void SetPlayerName(string newValue)
    {
        GameManager.instance.SetPlayerName(nameInput.text);
        playButton.interactable = !string.IsNullOrEmpty(newValue);
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
