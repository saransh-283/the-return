using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Player player;

    public TMP_InputField textEntryField;
    public TextMeshProUGUI logText;
    public TextMeshProUGUI currentText;
    public Image backgroundImage;

    [TextArea]
    public string introText;

    public Action[] actions;
    // Start is called before the first frame update
    void Start()
    {
        logText.text = introText;
        DisplayLocation();
        textEntryField.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && textEntryField.text != "")
        {
            TextEntered();
        }
    }

    public void DisplayLocation(bool additive = false) 
    {
        string description = player.currentLocation.description + "\n";
        description += player.currentLocation.GetConnectionsText();
        description += player.currentLocation.GetItemsText();
        if (additive) currentText.text += "\n" + description;
        else currentText.text = description;
        backgroundImage.sprite = player.currentLocation.backgroundImage;
    }

    public void TextEntered()
    {
        LogCurrentText();
        ProcessInput(textEntryField.text);
        textEntryField.text = "";
        textEntryField.ActivateInputField();
    }

    void LogCurrentText()
    {
        logText.text += "\n\n";
        logText.text += currentText.text;
        
        logText.text += "\n\n";
        logText.text += "<color=#aaccaaff>" + textEntryField.text + "</color>";
    }

    void ProcessInput(string input)
    {
        input = input.ToLower();

        char[] delimiter = { ' ' };
        string[] seperatedWords = input.Split(delimiter);

        foreach (Action action in actions) { 
            if(action.keyword.ToLower() == seperatedWords[0])
            {
                currentText.text = "";
                if(seperatedWords.Length > 1)
                {
                    action.RespondToInput(this, seperatedWords[1]);
                    
                }
                else
                {
                    action.RespondToInput(this, "");
                }
                return;
            }
        }

        currentText.text = "Nothing happens. (having trouble? Type 'Help')";
    }
}
