﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public static Chest instance;
    private ChestUI chestUI;

    [SerializeField] private AllItemsAsset allItems;

    public List<GameObject> l;
    public int space = 12;  // Amount of slots in chest
    
    // Callback which is triggered when
    // an item gets added/removed.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public AllItemsAsset AllItems { get => allItems; }

    // Current list of items in chest
    public List<GameObject> items = new List<GameObject>();

    void Start()
    {
        instance = this;
        chestUI = ChestUI.instance;
        l = new List<GameObject>();
        
        for(int i = 0; i < 10; i++)
        {
            items.Add(AllItems.AllItems[i]);
        }
        chestUI.SetItems(items);
    }


    // Add a new item. If there is enough room we
    // return true. Else we return false.
    public bool Add(GameObject item)
    {
        foreach (GameObject O in items) //Check if the item is already in items or not
        {
            if (O == item) return false;
        }

        // Check if out of space
        if (items.Count < space)
        {
            items.Add(item);    // Add item to list

            return true;
        }

        return false;
    }

    // Remove an item
    public void Remove(GameObject item)
    {
        items.Remove(item);     // Remove item from list

        // Trigger callback
    }

}
