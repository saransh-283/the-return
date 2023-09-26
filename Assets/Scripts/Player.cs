using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject endUI;
    public Location currentLocation;
    public Connection connection = null;

    public List<Item> inventory = new List<Item>();

    public bool ChangeLocation(GameController controller, string connectionNoun)
    {
        connection = currentLocation.GetConnection(connectionNoun);
        if (connection != null)
        {
            if (connection.connectionEnabled)
            {
                currentLocation = connection.location;
                return true;
            }
        }
        return false;
    }

    public void Teleport (GameController controller, Location destination)
    {
        currentLocation = destination;
        StartCoroutine(controller.effects.LocationChangeFadeOutIn(() =>
        {
            controller.backgroundImage.sprite = controller.player.currentLocation.backgroundImage;
            gameUI.SetActive(false);
            endUI.SetActive(true);
        }, controller));
    }

    internal bool CanUseItem(GameController controller, Item item)
    {
        if(item.targetItem == null) {
            return true;
        }

        if (HasItem(item.targetItem))
        {
            return true;
        }

        if (currentLocation.HasItem(item.targetItem))
        {
            return true;
        }


        return false;
    }

    private bool HasItem(Item itemToCheck)
    {
        foreach (Item item in inventory)
        {
            if (item == itemToCheck && item.itemEnabled)
            {
                return true;
            }
        }
        return false;
    }

    internal bool CanTalkToItem(GameController controller, Item item)
    {
        return item.playerCanTalkTo;
    }

    internal bool CanGiveToItem(GameController controller, Item item)
    {
        return item.playerCanGiveTo;
    }

    internal bool HasItemByName(string noun)
    {
        foreach(Item item in inventory)
        {
            if(item.itemName.ToLower() == noun.ToLower())
            {
                return true;
            }
        }
        return false;
    }

    internal bool CanReadItem(GameController controller, Item item)
    {
        return item.playerCanRead;
    }
}
