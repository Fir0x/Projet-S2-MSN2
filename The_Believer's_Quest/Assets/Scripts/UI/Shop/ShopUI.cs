using System.Collections.Generic;
using UnityEngine;
//Maxence && Nicolas L
public class ShopUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject shopUI;

    public static ShopUI instance;

    private List<GameObject> itemsInShop;

    ShopSlot[] slots;

    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        instance = this;
        
        slots = itemsParent.GetComponentsInChildren<ShopSlot>();
    }

    public void SetItems(List<GameObject> items)
    {
        itemsInShop = items;
        UpdateUI();
    }

    public void EnableUI()
    {
        shopUI.SetActive(!shopUI.activeSelf);
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < itemsInShop.Count)
            {
                slots[i].AddItem(itemsInShop[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
