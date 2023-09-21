using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Help")]
public class Help : Action
{
    public override void RespondToInput(GameController controller, string noun)
    {
        controller.currentText.text += "Type a Verb followed by a Noun (eg. \"go north\")";
        controller.currentText.text += "\nAllowed verbs: \nGo, Examine, Get, Use, Inventory, TalkTo, Say, Help, Give";
    }
}
