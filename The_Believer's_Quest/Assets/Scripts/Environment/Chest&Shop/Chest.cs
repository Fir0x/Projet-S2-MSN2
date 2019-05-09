using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//Nicolas I
public class Chest : ItemChooser, IInteractableObject
{
    private AllItemsAsset allItems;
    
    public void SetChest(AllItemsAsset allItems)
    {
        this.allItems = allItems;
    }

    public void Interaction()
    {
        //Call ItemPanel initialization
        throw new System.NotImplementedException();
    }
}
