using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "UnlockedItems", menuName = "Items/UnlockedItems")]
public class UnlockedItemsAsset : ScriptableObject
{
    [SerializeField] private List<GameObject> unlocked;
    [SerializeField] private List<GameObject> locked;

    public List<GameObject> Unlocked { get => unlocked; set => unlocked = value; }
    public List<GameObject> Locked { get => locked; set => locked = value; }
}
