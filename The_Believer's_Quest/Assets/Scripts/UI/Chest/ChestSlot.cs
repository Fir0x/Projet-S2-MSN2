using UnityEngine;
using UnityEngine.UI;

/* Sits on all InventorySlots. */
//Maxence && Nicolas L
public class ChestSlot : MonoBehaviour
{
    private Inventory inventory;
    private Chest chest;
    private ChestUI chestUI;
    public Image icon;          // Reference to the Icon image

    GameObject item;  // Current item in the slot

    private void Start()
    {
        inventory = Inventory.instance;
        chest = Chest.instance;
        chestUI = ChestUI.instance;
    }
    // Add item to the slot
    public void AddItem(GameObject newItem)
    {
        item = newItem;
        if (item.CompareTag("Object"))
            icon.sprite = item.GetComponent<Object>().ObjectsAsset.Sprite;

        else if (item.CompareTag("Weapon"))
            icon.sprite = item.GetComponent<WeaponItem>().WeaponAsset.Sprite;

        icon.enabled = true;
    }

    // Clear the slot
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    // Called when the item is pressed
    public void UseItem()
    {
        if (item != null && Player.instance.PlayerAsset.WeaponsList[0] != item && Player.instance.PlayerAsset.WeaponsList[1] != item)
        {
            if (inventory.Add(item))
                chest.Remove(item);
        }
    }

}
