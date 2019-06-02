using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    private ShopUI shopUI;

    [SerializeField] private AllItemsAsset allItems;

    public int space = 12;  // Amount of slots in chest

    public AllItemsAsset AllItems { get => allItems; }

    // Current list of items in chest
    public List<GameObject> items = new List<GameObject>();

    void Start()
    {
        instance = this;
        shopUI = ShopUI.instance;

        for (int i = 0; i < 10; i++)
        {
            items.Add(AllItems.AllItems[i]);
        }

        shopUI.SetItems(items);
    }


    // Add a new item. If there is enough room we
    // return true. Else we return false.
    public bool Add(GameObject item)
    {
        if (items.Count < space)
        {
            items.Add(item);
            shopUI.UpdateUI();
            return true;
        }
        else
        {
            return false;
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
