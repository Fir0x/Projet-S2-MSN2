using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicolas I && Maxence && Nicolas L
[CreateAssetMenu (fileName = "AllItems", menuName = "Items/AllItem")]
public class AllItemsAsset : ScriptableObject
{
    [SerializeField] private List<GameObject> allItems;

    public List<GameObject> AllItems { get => allItems; set => allItems = value; }
}
