using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicolas I
[CreateAssetMenu (fileName = "AllItem", menuName = "Items/AllItem")]
public class AllItemsAsset : ScriptableObject
{
    [SerializeField] private List<GameObject> allItems;

    public List<GameObject> AllItems { get => allItems; set => allItems = value; }
}
