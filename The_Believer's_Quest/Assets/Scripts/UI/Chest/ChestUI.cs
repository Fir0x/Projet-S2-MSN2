using System.Collections.Generic;
using UnityEngine;

public class ChestUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject chestUI;

    public static ChestUI instance;

    private List<GameObject> itemsInChest;

    ChestSlot[] slots;

    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
  
        instance = this;

        slots = itemsParent.GetComponentsInChildren<ChestSlot>();
    }

    public void SetItems(List<GameObject> items)
    {
        itemsInChest = items;
    }

    public void EnableUI()
    {
        chestUI.SetActive(!chestUI.activeSelf);
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < itemsInChest.Count)
            {
                slots[i].AddItem(itemsInChest[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
