using UnityEngine;
using UnityEngine.UI;

/* Sits on all InventorySlots. */

public class ShopSlot : MonoBehaviour
{
    private Inventory inventory;
    private Shop shop;
    private ShopUI shopUI;
    public Image icon;          // Reference to the Icon image

    GameObject item;  // Current item in the slot

    private void Start()
    {
        inventory = Inventory.instance;
        shop = Shop.instance;
        shopUI = ShopUI.instance;
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
        int gold = Player.instance.PlayerAsset.Gold;
        int price = 0;
        if (item.CompareTag("Object"))
            price = item.GetComponent<Object>().ObjectsAsset.Price;
        else if (item.CompareTag("Weapon"))
            price = item.GetComponent<WeaponItem>().WeaponAsset.Price;

        if (item != null && price <= gold)
        {
            if (inventory.Add(item))
            {
                shop.Remove(item);
                Player.instance.PlayerAsset.Gold -= price;
                UIController.uIController.changeGold.Invoke();
            }
        }
    }

}
