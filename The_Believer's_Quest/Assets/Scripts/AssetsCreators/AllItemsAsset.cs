using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "AllItem", menuName = "Item/AllItem")]
public class AllItemsAsset : ScriptableObject
{
    [SerializeField] private List<GameObject> allItems;

    public List<GameObject> AllItems { get => allItems; set => allItems = value; }
}
