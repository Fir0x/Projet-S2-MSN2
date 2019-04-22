using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Chest : MonoBehaviour
{
    [SerializeField] private List<GameObject> allItems;

    private GameObject[] ChooseContent()
    {
        int nbItems = Random.Range(1, 5);
        GameObject[] items = new GameObject[nbItems];
        int index = 0;

        float minRarity = Random.Range(0.2f, ((10 / 7) - 0.19f) / 2);
        float maxRarity = Random.Range(((10 / 7) - 0.2f) / 2, 10 / 7);
        List<GameObject> choosen = (from item in allItems
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
