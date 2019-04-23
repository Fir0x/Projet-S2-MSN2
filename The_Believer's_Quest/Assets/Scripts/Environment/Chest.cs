using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Chest : ItemChooser
{
    [SerializeField] private AllItemsAsset allItems;
    private List<GameObject> itemList;

    public AllItemsAsset AllItems { get => allItems; set => allItems = value; }

    public void Interaction()
    {
        //Call ItemPanel initialization
        throw new System.NotImplementedException();
    }
}
