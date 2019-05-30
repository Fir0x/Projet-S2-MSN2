using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            print("should not do that" ); //DEBUG
            return;
        }

        instance = this;
    }

    // Callback which is triggered when
    // an item gets added/removed.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 4;  // Amount of slots in inventory

    // Current list of items in inventory
    public List<ObjectsAsset> items = new List<ObjectsAsset>();

    // Add a new item. If there is enough room we
    // return true. Else we return false.
    public bool Add(ObjectsAsset item)
    {

        // Check if out of space
        if (items.Count < space)
        {
            items.Add(item);    // Add item to list
            // Trigger callback
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();

            return true;
        }

        return false;
    }

    // Remove an item
    public void Remove(ObjectsAsset item)
    {
        items.Remove(item);     // Remove item from list

        // Trigger callback
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}