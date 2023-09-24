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

    public Effects effects;
    // Start is called before the first frame update
    void Start()
    {
        logText.text = introText;
        LocationTransition();
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

    public void LocationTransition(bool additive = false) 
    {
        StartCoroutine(effects.LocationChangeFadeOutIn(() =>
        {
            DisplayLocation(additive);
            backgroundImage.sprite = player.currentLocation.backgroundImage;
        }, this));
    }

    public void DisplayLocation(bool additive)
    {
        string description = player.currentLocation.description + "\n";
        description += player.currentLocation.GetConnectionsText();
        description += player.currentLocation.GetItemsText();
        if (additive) currentText.text += "\n" + description;
        else currentText.text = description;
    }

    public void TextEntered()
    {
        LogCurrentText();
        ProcessInput(textEntryField.text);
        StartCoroutine(effects.FadeOutAndReset(textEntryField));
        textEntryField.ActivateInputField();
    }

    void LogCurrentText()
    {
        logText.text += "\n\n";
        logText.text += currentText.text;
        
        logText.text += "\n";
        logText.text += "<b><font=\"Anonymous SDF\"><color=#aaccaaff>" + textEntryField.text + "</color></font></b>";
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
