using UnityEngine;
using UnityEngine.UI;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour
{

    public Image icon;          // Reference to the Icon image

    GameObject item;  // Current item in the slot

    // Add item to the slot
    public void AddItem(GameObject newItem)
    {
        item = newItem;
        
        if(item.CompareTag("Object"))
        {
            icon.sprite = item.GetComponent<Object>().ObjectsAsset.Sprite;
        }
        else if (item.CompareTag("Weapon"))
        {
            icon.sprite = item.GetComponent<WeaponItem>().WeaponAsset.Sprite;
        }

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
        if (GameObject.Find("Player").GetComponent<Player>().RoomType == Board.Type.Chest)
        {
            if (item != null)
            {
                if (Chest.instance.Add(item))
                {
                    Inventory.instance.Remove(item);
                }
            }
        }

        if (GameObject.Find("Player").GetComponent<Player>().RoomType == Board.Type.Shop)
        {
            if (item != null)
            {
                int price = 0;
                if (item.CompareTag("Object"))
                    price = item.GetComponent<Object>().ObjectsAsset.Price;
                else if (item.CompareTag("Weapon"))
                    price = item.GetComponent<WeaponItem>().WeaponAsset.Price;

                if (Shop.instance.Add(item))
                {
                    Inventory.instance.Remove(item);
                    Player.instance.PlayerAsset.Gold += price / 2;
                    UIController.uIController.changeGold.Invoke();
                }
            }
        }
    }

}