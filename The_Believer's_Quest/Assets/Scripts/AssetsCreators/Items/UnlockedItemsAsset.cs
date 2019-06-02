using System.Collections.Generic;
using UnityEngine;
//Nicolas L
[CreateAssetMenu (fileName = "UnlockedItems", menuName = "Items/UnlockedItems")]
public class UnlockedItemsAsset : ScriptableObject
{
    [SerializeField] private List<GameObject> unlocked;
    [SerializeField] private List<GameObject> locked;

    public List<GameObject> Unlocked { get => unlocked; set => unlocked = value; }
    public List<GameObject> Locked { get => locked; set => locked = value; }

    public void CheckDuplicate() //Avoid error from developers 
    {
        int i = 0;
        while (i < locked.Count)
        {
            List<GameObject> all = locked.FindAll(itm => itm == locked[i]);
            int occ = all.Count;
            GameObject item = all[0];
            for (int n = 0; n < occ - 1; n++)
                locked.Remove(item);

            i++;
        }
    }
}
