using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

//Nicolas L && Maxence
public class ItemChooser : MonoBehaviour
{
    public List<GameObject> ChooseContentChest(AllItemsAsset availableItems, List<GameObject> unlocked)
    {
        int nbItems = 2;
        List<GameObject> items = new List<GameObject>();
        float minRarity = 0;//Random.Range(0.2f, ((10 / 7) - 0.19f) / 2);
        float maxRarity = 1000;//Random.Range(((10 / 7) - 0.2f) / 2, 10 / 7);
        int len = availableItems.AllItems.Count;
        List<GameObject> allItems = availableItems.AllItems;

        int i;
        bool stillWeapon = true;
        bool stillObject = true;
        while (nbItems > 0)
        {
            i = Random.Range(0, len);
            if (stillWeapon && unlocked.Contains(allItems[i]) && allItems[i].CompareTag("Weapon")) 
            {
                stillWeapon = false;
                nbItems -= 1;
                items.Add(allItems[i]);
            }
            if (stillObject && allItems[i].CompareTag("Object")  && !allItems[i].GetComponent<Object>().ObjectsAsset.passive)
            {
                stillObject = false;
                nbItems -= 1;
                items.Add(allItems[i]);
            }
        }

        return items;

    }

    public List<GameObject> ChooseContentShop(AllItemsAsset availableItems, UnlockedItemsAsset unlockedItems)
    {
        int nbItems = 5;
        List<GameObject> items = new List<GameObject>();
        float minRarity = 0;//Random.Range(0.2f, ((10 / 7) - 0.19f) / 2);
        float maxRarity = 1000;//Random.Range(((10 / 7) - 0.2f) / 2, 10 / 7);
        List<GameObject> allItems = availableItems.AllItems;
        List<GameObject> unlocked = unlockedItems.Unlocked;

        int len1 = allItems.Count;
        int len2 = unlocked.Count;

        int i;
        int j;
        int stillWeapon = 2;
        int stillObject = 3;
        bool active = true;

        while (nbItems > 0)
        {
            i = Random.Range(0, len1);
            j = Random.Range(0, len2);

            if (stillWeapon > 0 && 100 / unlocked[j].GetComponent<WeaponItem>().WeaponAsset.Price >= minRarity &&
                                          100 / unlocked[j].GetComponent<WeaponItem>().WeaponAsset.Price <= maxRarity && !items.Contains(unlocked[j]))
            {
                stillWeapon -= 1;
                nbItems -= 1;
                items.Add(unlocked[j]);
            }
            if (active && stillObject > 0 && allItems[i].CompareTag("Object") && !allItems[i].GetComponent<Object>().ObjectsAsset.passive && 100 / allItems[i].GetComponent<Object>().ObjectsAsset.Price >= minRarity &&
                                          100 / allItems[i].GetComponent<Object>().ObjectsAsset.Price <= maxRarity && !allItems[i].GetComponent<Object>().ObjectsAsset.passive && !items.Contains(allItems[i]))
            {
                stillObject -= 1;
                nbItems -= 1;
                items.Add(allItems[i]);
                active = false;
            }

            if (!active && stillObject > 0 && allItems[i].CompareTag("Object") && allItems[i].GetComponent<Object>().ObjectsAsset.passive && 100 / allItems[i].GetComponent<Object>().ObjectsAsset.Price >= minRarity &&
                                           100 / allItems[i].GetComponent<Object>().ObjectsAsset.Price <= maxRarity && allItems[i].GetComponent<Object>().ObjectsAsset.passive && !items.Contains(allItems[i]) && !Inventory.instance.items.Contains(allItems[i]))
            {
                stillObject -= 1;
                nbItems -= 1;
                items.Add(allItems[i]);
            }
        }

        return items;
    }
}
