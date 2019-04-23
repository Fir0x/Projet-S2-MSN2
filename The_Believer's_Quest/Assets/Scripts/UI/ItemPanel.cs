using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{ 
    private List<GameObject> items = new List<GameObject>();

    [SerializeField] private Transform parent;

    private SlotController[] slots;

    public Transform Parent { get => parent; set => parent = value; }

    public void Init(AllItemsAsset allItems)
    {
        items = GetComponent<ItemChooser>().ChooseContent(allItems);
        slots = Parent.GetComponentsInChildren<SlotController>();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
                slots[i].AddItem(items[i]);
            else
                slots[i].ClearSlot();
        }
    }

}
