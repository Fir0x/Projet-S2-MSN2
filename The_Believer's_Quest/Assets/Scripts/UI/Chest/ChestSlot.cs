﻿using UnityEngine;
using UnityEngine.UI;

/* Sits on all InventorySlots. */

public class ChestSlot : MonoBehaviour
{

    public Image icon;          // Reference to the Icon image

    ObjectsAsset item;  // Current item in the slot

    // Add item to the slot
    public void AddItem(ObjectsAsset newItem)
    {
        item = newItem;

        icon.sprite = item.Sprite;
        icon.enabled = true;
    }

    // Clear the slot
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    // Called when the item is pressed
    public void UseItem()
    {
        if (item != null)
        {
            if (!Inventory.instance.Add(item))
            {
                Chest.instance.Remove(item);
                ClearSlot();
            }
        }
    }

}