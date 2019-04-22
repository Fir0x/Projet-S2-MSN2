using UnityEngine;

[CreateAssetMenu (fileName = "NewWeapon", menuName = "Items/Weapon")]
public class WeaponAsset : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private int price;
    [SerializeField] private int damage;
    [SerializeField] private float cooldown;
    [SerializeField] private float reloadingTime;
    [SerializeField] private int loader;
    [SerializeField] private int loaderCappacity;
    [SerializeField] private int maxAmmunitions;
    [SerializeField] private int ammunitions;
    [SerializeField] private int speed;
    [SerializeField] private bool shotgun;
    [SerializeField] private bool railgun;
    [SerializeField] private bool cqc;
    [SerializeField] private bool circleshot;

    public string WeaponName { get => weaponName; set => weaponName = value; }
    public int Price { get => price; set => price = value; }
    public int Damage { get => damage; set => damage = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
    public float ReloadingTime { get => reloadingTime; set => reloadingTime = value; }
    public int Loader { get => loader; set => loader = value; }
    public int LoaderCappacity { get => loaderCappacity; set => loaderCappacity = value; }
    public int MaxAmmunitions { get => maxAmmunitions; set => maxAmmunitions = value; }
    public int Ammunitions { get => ammunitions; set => ammunitions = value; }
    public int Speed { get => speed; set => speed = value; }
    public bool Shotgun { get => shotgun; set => shotgun = value; }
    public bool Railgun { get => railgun; set => railgun = value; }
    public bool Cqc { get => cqc; set => cqc = value; }
    public bool Circleshot { get => circleshot; set => circleshot = value;}
}
