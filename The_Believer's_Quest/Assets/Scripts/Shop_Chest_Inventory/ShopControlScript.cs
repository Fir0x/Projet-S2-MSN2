using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Text moneyAmountText;
    public Text itemPrice;
    public Text areaText;
    [SerializeField] private PlayerAsset playerasset;
    private int GoldAmountP;
    private int DiamondAmountP;

    public PlayerAsset Playerasset
    {
        get  => playerasset; set => playerasset = value; 
    }

    void Start()
    {
        GoldAmountP = playerasset.Gold;
        DiamondAmountP = playerasset.Diamond;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
