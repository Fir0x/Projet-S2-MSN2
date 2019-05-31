using UnityEngine;

public class ChestUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject chestUI;

    Chest chest;

    ChestSlot[] slots;

    void Start()
    {
        chest = Chest.instance;
        chest.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<ChestSlot>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            chestUI.SetActive(!chestUI.activeSelf);
        }
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
