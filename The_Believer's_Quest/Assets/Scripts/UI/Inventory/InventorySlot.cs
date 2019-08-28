using UnityEngine;
using UnityEngine.UI;

//Maxence && Nicolas L
/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour
{
    public Image icon;          // Reference to the Icon image
    [SerializeField] private GameObject goldShow;

    GameObject item;  // Current item in the slot

    public GameObject GoldShow { get => goldShow; set => goldShow = value; }

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

    public void ShowSellPrice(bool show)
    {
        if (show)
        {
            goldShow.SetActive(true);
            if (item.CompareTag("Object"))
                goldShow.GetComponent<Text>().text = "" + (item.GetComponent<Object>().ObjectsAsset.Price / 2);
            else
                goldShow.GetComponent<Text>().text = "" + (item.GetComponent<WeaponItem>().WeaponAsset.Price / 2);
        }
        else
            goldShow.SetActive(false);
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
        if (Player.instance.RoomType == Board.Type.Chest)
        {
            if (item != null)
            {
                if (Chest.instance.Add(item))
                    Inventory.instance.Remove(item);
            }
        }

        if (Player.instance.RoomType == Board.Type.Shop)
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
                    if (item.CompareTag("Weapon"))
                        item.GetComponent<WeaponItem>().WeaponAsset.ResetWeapon();
                    Inventory.instance.Remove(item);
                    Player.instance.PlayerAsset.Gold += price / 2;
                    UIController.uIController.changeGold.Invoke();
                }
            }
        }
    }

}