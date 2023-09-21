using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Read")]
public class Read : Action
{
    public override void RespondToInput(GameController controller, string noun)
    {
        if (ReadItem(controller, controller.player.currentLocation.items, noun))
        {
            return;
        }
        controller.currentText.text = "The " + noun + " has nothing written on it";
    }

    private bool ReadItem(GameController controller, List<Item> items, string noun)
    {
        foreach (Item item in items)
        {
            if (item.itemEnabled)
            {
                if (item.itemName == noun)
                {
                    if (item.InteractWith(controller, "read"))
                    {
                        return true;
                    }
                    controller.currentText.text = item.description;

                    return true;
                }
            }
        }
        return false;
    }
}
