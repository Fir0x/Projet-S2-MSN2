using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControl : MonoBehaviour
{
    public Text moneyAmountText;
    public Text itemPrice;
    public Text areaText;
    [SerializeField] private PlayerAsset playerasset;
    private int GoldAmountP;
    private int DiamondAmountP;
    [SerializeField] private ObjectsAsset objectasset;
    

    public PlayerAsset Playerasset
    {
        get  => playerasset; set => playerasset = value; 
    }

    public ObjectsAsset Objectasset
    {
        get => objectasset; set => objectasset = value; 
    }

    void Start()
    {
        GoldAmountP = playerasset.Gold;
        DiamondAmountP = playerasset.Diamond;
    }

    void Update()
    {
        
    }
}
