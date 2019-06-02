using UnityEngine;
using UnityEngine.UI;

/* Sits on all InventorySlots. */

public class ShopSlot : MonoBehaviour
{
    private Inventory inventory;
    private Shop shop;
    private ShopUI shopUI;
    public Image icon;          // Reference to the Icon image
    private bool isHub;

    GameObject item;  // Current item in the slot

    private void Start()
    {
        inventory = Inventory.instance;
        shop = Shop.instance;
        shopUI = ShopUI.instance;

        isHub = shop.isHub;
    }
    // Add item to the slot
    public void AddItem(GameObject newItem)
    {
        item = newItem;
        if (isHub)
        {
            icon.enabled = true;
        }
        else
        {
            if (item.CompareTag("Object"))
                icon.sprite = item.GetComponent<Object>().ObjectsAsset.Sprite;
            else if (item.CompareTag("Weapon"))
                icon.sprite = item.GetComponent<WeaponItem>().WeaponAsset.Sprite;
            icon.enabled = true;
        }
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
        if (isHub)
        {
            int diamond = Player.instance.PlayerAsset.Diamond;
            int price = item.GetComponent<WeaponItem>().WeaponAsset.DiamondPrice;

            if (item != null && price <= diamond)
            {
                shop.Remove(item);
                Player.instance.PlayerAsset.Diamond -= price;
                shop.UnlockedItems.Unlocked.Add(item);
                shop.UnlockedItems.Locked.Remove(item);
                UIController.uIController.changeDiamond.Invoke();
            }
        }
        else
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

}
