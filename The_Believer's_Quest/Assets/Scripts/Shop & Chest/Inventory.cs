using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    public static Inventory instance;

    public int space = 12;  // Amount of slots in chest

    
    // Current list of items in inventory
    public List<GameObject> items = new List<GameObject>();

    void Start()
    {
        instance = this;

        print("Inventory Start()");
    }

    // Add a new item. If there is enough room we
    // return true. Else we return false.
    public bool Add(GameObject item)
    {
        // Check if out of space
        if (items.Count < space)
        {
            items.Add(item);    // Add item to list
            print("inventaire : " + items.Count);
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