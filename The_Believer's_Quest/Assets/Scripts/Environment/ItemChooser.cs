using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//Nicolas I
public class ItemChooser : MonoBehaviour
{
    public List<GameObject> ChooseContent(AllItemsAsset allItemsAsset)
    {
        int nbItems = Random.Range(1, 5);
        List<GameObject> items = new List<GameObject>();

        float minRarity = Random.Range(0.2f, ((10 / 7) - 0.19f) / 2);
        float maxRarity = Random.Range(((10 / 7) - 0.2f) / 2, 10 / 7);
        List<GameObject> choosen = (from item in allItemsAsset.AllItems
                                    where (100 / item.GetComponent<Weapon>().GetAsset().Price >= minRarity &&
                                          100 / item.GetComponent<Weapon>().GetAsset().Price <= maxRarity) ||
                                          (100 / item.GetComponent<Object>().GetAsset().Price >= minRarity &&
                                          100 / item.GetComponent<Object>().GetAsset().Price <= maxRarity)
                                    select item).ToList();

        for (int i = 0; i < nbItems; i++)
        {
            int itemIndex = Random.Range(0, choosen.Count);
            items[i] = choosen[itemIndex];
            choosen.RemoveAt(itemIndex);
        }

        return items;
    }
}
