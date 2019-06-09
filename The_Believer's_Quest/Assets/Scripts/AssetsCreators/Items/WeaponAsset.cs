using UnityEngine;
//Nicolas I && Maxence && Nicolas L
[CreateAssetMenu (fileName = "NewWeapon", menuName = "Items/Weapon")]
public class WeaponAsset : ScriptableObject
{
    public enum WeaponType
    {
        CQC,
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
    [SerializeField] private int loaderCapacity;
    [SerializeField] private int maxAmmunitions;
    [SerializeField] private int ammunitions;
    [SerializeField] private int speed;
    [SerializeField] private WeaponType type;
    [SerializeField] private int nbbulletsbyshot;
    [SerializeField] private Sprite projectile;
    [SerializeField] private int diamondPrice;
    [SerializeField] private AudioClip clip;

    //Memory
    private int memoDamage;
    private float memoCooldown;
    private float memoReloadingTime;
    private int memoLoader;
    private int memoAmmo;
    private int memoSpeed;

    public Sprite Sprite { get => sprite; set => sprite = value; }
    public int Price { get => price; set => price = value; }
    public int Damage { get => damage; set => damage = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
    public float ReloadingTime { get => reloadingTime; set => reloadingTime = value; }
    public int Loader { get => loader; set => loader = value; }
    public int LoaderCappacity { get => loaderCapacity; set => loaderCapacity = value; }
    public int MaxAmmunitions { get => maxAmmunitions; set => maxAmmunitions = value; }
    public int Ammunitions { get => ammunitions; set => ammunitions = value; }
    public int Speed { get => speed; set => speed = value; }
    public WeaponType Type { get => type; set => type = value; }
    public int Nbbulletsbyshot { get => nbbulletsbyshot; set => nbbulletsbyshot = value;}
    public Sprite Bullet{ get => projectile; set => projectile = value;}
    public int DiamondPrice { get => diamondPrice; set => diamondPrice = value; }
    public AudioClip Clip { get => clip; set => clip = value; }

    private void Awake()
    {
        memoDamage = damage;
        memoCooldown = cooldown;
        memoReloadingTime = reloadingTime;
        memoLoader = loader;
        memoAmmo = ammunitions;
        memoSpeed = speed;
    }

    public void ResetWeapon()
    {
        damage = memoDamage;
        cooldown = memoCooldown;
        reloadingTime = memoReloadingTime;
        loader = memoLoader;
        ammunitions = memoAmmo;
        speed = memoSpeed;
    }
}
