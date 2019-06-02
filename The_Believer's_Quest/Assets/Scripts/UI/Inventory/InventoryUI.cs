using System.Collections.Generic;
using UnityEngine;
//Maxence && Nicolas L
public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;   
    public GameObject inventoryUI;

    public static InventoryUI instance;

    InventorySlot[] slots;

    List<GameObject> itemsInInventory;

    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        instance = this;

        itemsInInventory = new List<GameObject>();

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    public void AddItem(GameObject item)
    {
        itemsInInventory.Add(item);
        UpdateUI();
    }

    public void RemoveItem(GameObject item)
    {
        itemsInInventory.Remove(item);
        UpdateUI();
    }

    public void EnableUI()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        UpdateUI();
    }
    
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < itemsInInventory.Count)
            {
                slots[i].AddItem(itemsInInventory[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}