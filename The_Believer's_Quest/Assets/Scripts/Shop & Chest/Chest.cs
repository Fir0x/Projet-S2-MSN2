using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public static Chest instance;

    public int space = 12;  // Amount of slots in chest

    [SerializeField] public AllItemsAsset availableItels;
    
    // Callback which is triggered when
    // an item gets added/removed.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

   
    // Current list of items in chest
    public List<GameObject> items = new List<GameObject>();

    void Start()
    {
        if (instance != null)
        {
            print("should not do that"); //DEBUG
            return;
        }

        instance = this;

        /*List<GameObject> l = GetComponent<ItemChooser>().ChooseContent(availableItels);

        int i = Random.Range(0, l.Count);

        GameObject g = l[i];

        instance.Add(g);*/
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
            // Trigger callback
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();

            return true;
        }

        return false;
    }

    // Remove an item
    public void Remove(GameObject item)
    {
        items.Remove(item);     // Remove item from list

        // Trigger callback
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}
