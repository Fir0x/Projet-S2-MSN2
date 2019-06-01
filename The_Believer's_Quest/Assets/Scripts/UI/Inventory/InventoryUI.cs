using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public Transform itemsParent;   
    public GameObject inventoryUI;

    Inventory inventory;
    public static InventoryUI instance;

    InventorySlot[] slots; 

    void Start()
    {
        instance = this;
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;    
        
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    public void EnableUI()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
    
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)  
            {
                slots[i].AddItem(inventory.items[i]);   
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}