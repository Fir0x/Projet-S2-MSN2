using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int nbWeapons;
    private int nbItems;
    public static Inventory instance;
    private InventoryUI inventoryUI;

    public int space = 12;  // Amount of slots in chest

    
    // Current list of items in inventory
    public List<GameObject> items = new List<GameObject>();

    void Start()
    {
        nbItems = 0;
        nbWeapons = 0;
        inventoryUI = InventoryUI.instance;
        instance = this;
    }

    // Add a new item. If there is enough room we
    // return true. Else we return false.
    public bool Add(GameObject item)
    {
        if(item.CompareTag("Object") && item.GetComponent<Object>().ObjectsAsset.passive)
        {
            item.GetComponent<Object>().PassiveChange();
            return true;
        }
        // Check if out of space
        if (items.Count < space)
        {
            if (item.CompareTag("Object") && nbItems != 2)
            {
                nbItems += 1;
                items.Add(item);    // Add item to list
                InventoryUI.instance.AddItem(item);
                return true;
            }

            if (item.CompareTag("Weapon") && nbWeapons != 2)
            {
                nbWeapons += 1;
                items.Add(item);    // Add item to list
                InventoryUI.instance.AddItem(item);
                return true;
            }

        }

        return false;
    }

    // Remove an item
    public void Remove(GameObject item)
    {
        items.Remove(item);     // Remove item from list
        inventoryUI.RemoveItem(item);
        if (item.CompareTag("Object"))
            nbItems -= 1;
        else if (item.CompareTag("Weapon"))
            nbWeapons -= 1;
        // Trigger callback
    }

}