using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public static Chest instance;
    private ChestUI chestUI;

    [SerializeField] private AllItemsAsset allItems;

    public int space = 12;  // Amount of slots in chest

    public AllItemsAsset AllItems { get => allItems; }

    // Current list of items in chest
    public List<GameObject> items = new List<GameObject>();

    void Start()
    {
        instance = this;
        chestUI = ChestUI.instance;
        
        for(int i = 0; i < 10; i++)
        {
            items.Add(AllItems.AllItems[i]);
        }

        chestUI.SetItems(items);

    }

    private void Update()
    {
        print("Chest: " + items.Count);
    }

    // Add a new item. If there is enough room we
    // return true. Else we return false.
    public bool Add(GameObject item)
    {
        if (items.Count < space)
        {
            items.Add(item);
            chestUI.SetItems(items);
            return true;
        }
        else
        {
            return false;
        }
    }

    // Remove an item
    public void Remove(GameObject item)
    {
        items.Remove(item);     // Remove item from list
        chestUI.SetItems(items);
        // Trigger callback
    }

}
