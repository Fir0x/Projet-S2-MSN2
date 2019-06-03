﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Maxence && Nicolas L
public class Inventory : MonoBehaviour
{
    private int nbWeapons;
    private int nbItems;
    public static Inventory instance;
    private InventoryUI inventoryUI;

    public int space = 12;  // Amount of slots in chest

    
    // Current list of items in inventory
    public List<GameObject> items = new List<GameObject>();

    void Start()
    {
        nbItems = 0;
        nbWeapons = 0;
        inventoryUI = InventoryUI.instance;
        instance = this;
        if (!Application.isEditor) // fix a bug in build
            Player.instance.inventorySignal = true;
    }

    // Add a new item. If there is enough room we
    // return true. Else we return false.
    public bool Add(GameObject item)
    {
        if(item.CompareTag("Object") && item.GetComponent<Object>().ObjectsAsset.passive)
        {
            item.GetComponent<Object>().PassiveChange();
            return true;
        }
        // Check if out of space
        if (items.Count < space)
        {
            if (item.CompareTag("Object") && nbItems != 2)
            {
                nbItems += 1;
                items.Add(item);    // Add item to list
                InventoryUI.instance.AddItem(item);

                for (int i = 0; i < 2; i++)
                {
                    if (GetComponent<Player>().PlayerAsset.ObjectsList[i] == null || GetComponent<Player>().PlayerAsset.ObjectsList[i] == item)
                    {
                        GetComponent<Player>().PlayerAsset.ObjectsList[i] = item;
                        break;
                    }
                }

                return true;
            }

            if (item.CompareTag("Weapon") && nbWeapons != 2)
            {
                nbWeapons += 1;
                items.Add(item);    // Add item to list
                InventoryUI.instance.AddItem(item);

                for (int i = 0; i < 2; i++)
                {
                    if (GetComponent<Player>().PlayerAsset.WeaponsList[i] == null || GetComponent<Player>().PlayerAsset.WeaponsList[i] == item)
                    {
                        PlayerAsset playerAsset = GetComponent<Player>().PlayerAsset;
                        playerAsset.WeaponsList[i] = item;
                        if (!Player.instance.start)
                        {
                            Destroy(playerAsset.weaponsInstance[i]);
                            playerAsset.weaponsInstance[i] = Instantiate(item);
                        }
                        break;
                    }
                }
                return true;
            }

        }

        return false;
    }

    // Remove an item
    public void Remove(GameObject item)
    {
        items.Remove(item);     // Remove item from list
        inventoryUI.RemoveItem(item);
        if (item.CompareTag("Object"))
        {
            nbItems -= 1;
            for (int i = 0; i < 2; i++)
            {
                if (GetComponent<Player>().PlayerAsset.ObjectsList[i] == item)
                {
                    GetComponent<Player>().PlayerAsset.ObjectsList[i] = null;
                    break;
                }
            }
        }
        else if (item.CompareTag("Weapon"))
        {
            PlayerAsset playerAsset = GetComponent<Player>().PlayerAsset;
            nbWeapons -= 1;
            for (int i = 0; i < 2; i++)
            {
                if (playerAsset.WeaponsList[i] == item)
                {
                    playerAsset.WeaponsList[i] = null;
                    Destroy(playerAsset.weaponsInstance[i]);
                    playerAsset.weaponsInstance[i] = null;
                    break;
                }
            }
        }
          

    }

}