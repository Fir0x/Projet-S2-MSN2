using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    private ShopUI shopUI;

    [SerializeField] private AllItemsAsset allItems;
    [SerializeField] private UnlockedItemsAsset unlockedItems;

    public int space = 12;  // Amount of slots in chest

    public AllItemsAsset AllItems { get => allItems; }
    public UnlockedItemsAsset UnlockedItems { get => unlockedItems; set => unlockedItems = value; }

    // Current list of items in chest
    public List<GameObject> items = new List<GameObject>();

    void Start()
    {
        instance = this;
        shopUI = ShopUI.instance;

        items = shopUI.gameObject.GetComponent<ItemChooser>().ChooseContentShop(allItems, unlockedItems);

        shopUI.SetItems(items);
    }

    // Add a new item. If there is enough room we
    // return true. Else we return false.
    public bool Add(GameObject item)
    {
        if (items.Count < space)
        {
            items.Add(item);
            shopUI.SetItems(items);
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
        shopUI.SetItems(items);

        // Trigger callback
    }

}
