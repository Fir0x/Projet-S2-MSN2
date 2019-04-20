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
    [SerializeField] private bool railgun;

    public string WeaponName { get => weaponName; set => weaponName = value; }
    public int Price { get => price; set => price = value; }
    public int Damage { get => damage; set => damage = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
    public float ReloadingTime { get => reloadingTime; set => reloadingTime = value; }
    public int Loader { get => loader; set => loader = value; }
    public int LoaderCappacity { get => loaderCappacity; set => loaderCappacity = value; }
    public int MaxAmmunitions { get => maxAmmunitions; set => maxAmmunitions = value; }
    public int Ammunitions { get => ammunitions; set => ammunitions = value; }
    public bool Railgun { get => railgun; set => railgun = value; }
}
