using UnityEngine;
//Nicolas I
[CreateAssetMenu (fileName = "NewWeapon", menuName = "Items/Weapon")]
public class WeaponAsset : ScriptableObject
{
    public enum WeaponType
    {
        CQC,
        Railgun,
        Line,
        Shotgun,
        Circle
    }
    [SerializeField] private Sprite sprite;
    [SerializeField] private int price;
    [SerializeField] private int damage;
    [SerializeField] private float cooldown;
    [SerializeField] private float reloadingTime;
    [SerializeField] private int loader;
    [SerializeField] private int loaderCappacity;
    [SerializeField] private int maxAmmunitions;
    [SerializeField] private int ammunitions;
    [SerializeField] private int speed;
    [SerializeField] private WeaponType type;
    [SerializeField] private int nbbulletsbyshot;
    
    [SerializeField] private GameObject Object;

    public Sprite Sprite { get => sprite; set => sprite = value; }
    public int Price { get => price; set => price = value; }
    public int Damage { get => damage; set => damage = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
    public float ReloadingTime { get => reloadingTime; set => reloadingTime = value; }
    public int Loader { get => loader; set => loader = value; }
    public int LoaderCappacity { get => loaderCappacity; set => loaderCappacity = value; }
    public int MaxAmmunitions { get => maxAmmunitions; set => maxAmmunitions = value; }
    public int Ammunitions { get => ammunitions; set => ammunitions = value; }
    public int Speed { get => speed; set => speed = value; }
    public WeaponType Type { get => type; set => type = value; }
    public int Nbbulletsbyshot { get => nbbulletsbyshot; set => nbbulletsbyshot = value;}

    public GameObject gameobject { get => Object; set => Object = value; }
}
