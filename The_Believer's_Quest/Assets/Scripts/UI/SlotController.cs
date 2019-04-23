using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button itemButton;

    private GameObject item;
    private List<GameObject> items;

    public Image Icon { get => icon; set => icon = value; }
    public Button ItemButton { get => itemButton; set => itemButton = value; }

    public void Init(ref List<GameObject> items)
    {
        this.items = items;
    }

    public void AddItem(GameObject item)
    {
        this.item = item;
        Icon.sprite = item.GetComponent<Object>().GetAsset().Sprite;
        Icon.enabled = true;
        itemButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        Icon.sprite = null;
        Icon.enabled = false;
        ItemButton.interactable = false;
    }

    public void ButtonPress()
    {
        items.Remove(item);
    }
}
