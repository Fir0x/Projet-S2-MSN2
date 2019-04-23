using UnityEngine;
using Image = UnityEngine.UI.Image;

public class ItemControl : MonoBehaviour
{
    [SerializeField] public typeArea enumShop;
    public Image icon;
    
    public enum typeArea
    {
        Shop,
        Chest,
        Inventory,
    };
    
    public typeArea EnumShop
    {
        get => enumShop; set => enumShop = value; 
    }
    
    public void AddItem(ObjectsAsset itemadd)
    {
        icon.sprite = itemadd.Sprite;
        icon.enabled = true;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
