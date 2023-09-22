using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Get")]
public class Get : Action
{
    public override void RespondToInput(GameController controller, string noun)
    {
        foreach(Item item in controller.player.currentLocation.items)
        {
            Debug.Log(noun);
            if(item.itemEnabled && item.itemName ==  noun)
            {
                if (item.playerCanTake)
                {
                    controller.effects.effectsSource.PlayOneShot(item.getSound);
                    controller.player.inventory.Add(item);
                    controller.player.currentLocation.items.Remove(item);
                    controller.currentText.text = "You take the " + noun;
                    return;
                }
            }
        }
        controller.currentText.text = "You can't get that";
    }
}
