using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public Transform itemsParent;   
    public GameObject inventoryUI;

    private Inventory inventory;

    public static InventoryUI instance;

    InventorySlot[] slots; 

    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        instance = this;
        inventory = gameObject.GetComponent<Inventory>();
        
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