using UnityEngine;
using UnityEngine.UI;

/* Sits on all InventorySlots. */
//Maxence && Nicolas L
public class ShopSlot : MonoBehaviour
{
    private Inventory inventory;
    private Shop shop;
    private ShopUI shopUI;
    public Image icon;          // Reference to the Icon image
    private bool isHub;

    GameObject item;  // Current item in the slot

    public GameObject goldGO;
    public GameObject diamsGO;
    public Text gold;
    public Text diamond;

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
        if (GameObject.Find("Player").GetComponent<Player>().PlayerAsset.Floor == 0)
        {
            if (item != null)
            {
                print(item.name);
                if (item.GetComponent<WeaponItem>().WeaponAsset.Sprite != null)
                {
                    icon.sprite = item.GetComponent<WeaponItem>().WeaponAsset.Sprite;
                    diamsGO.SetActive(true);
                    diamond.text = item.GetComponent<WeaponItem>().WeaponAsset.DiamondPrice + "";
                    icon.enabled = true;
                }
            }
        }
        else
        {
            diamsGO.SetActive(false);
            if (item.CompareTag("Object"))
            {
                icon.sprite = item.GetComponent<Object>().ObjectsAsset.Sprite;
                goldGO.SetActive(true);
                gold.text = item.GetComponent<Object>().ObjectsAsset.Price + "";
            }
            else if (item.CompareTag("Weapon"))
            {
                icon.sprite = item.GetComponent<WeaponItem>().WeaponAsset.Sprite;
                goldGO.SetActive(true);
                gold.text = item.GetComponent<WeaponItem>().WeaponAsset.Price + "";
            }
                icon.enabled = true;
        }
    }

    // Clear the slot
    public void ClearSlot()
    {
        item = null;
        goldGO.SetActive(false);
        diamsGO.SetActive(false);
        icon.sprite = null;
        icon.enabled = false;
    }

    // Called when the item is pressed
    public void UseItem()
    {
        if (GameObject.Find("Player").GetComponent<Player>().PlayerAsset.Floor == 0)
        {
            int diamond = Player.instance.PlayerAsset.Diamond;
            int price = item.GetComponent<WeaponItem>().WeaponAsset.DiamondPrice;

            if (item != null && price <= diamond)
            {
                shop.UnlockedItems.Locked.Remove(item);
                shop.UnlockedItems.Unlocked.Add(item);
                shop.Remove(item);
                Player.instance.PlayerAsset.Diamond -= price;
                UIController.uIController.changeDiamond.Invoke();
                Saver.SavePlayerData(Player.instance.PlayerAsset, shop.UnlockedItems.Unlocked);
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

            if (item != null && price <= gold && GameObject.Find("Player").GetComponent<Player>().PlayerAsset.WeaponsList[0] != item && GameObject.Find("Player").GetComponent<Player>().PlayerAsset.WeaponsList[1] != item)
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
