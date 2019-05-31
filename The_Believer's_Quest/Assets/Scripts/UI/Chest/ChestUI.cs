using UnityEngine;

public class ChestUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject chestUI;

    Chest chest;

    public static ChestUI instance;

    ChestSlot[] slots;

    void Start()
    {
        instance = this;
        chest = Chest.instance;
        chest.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<ChestSlot>();
    }

    public void EnableUI()
    {
        chestUI.SetActive(!chestUI.activeSelf);
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < chest.items.Count)
            {
                slots[i].AddItem(chest.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
