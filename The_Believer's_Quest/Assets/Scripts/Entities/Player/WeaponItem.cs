using UnityEngine;
//Sarah
public class WeaponItem : MonoBehaviour
{
    [SerializeField] private WeaponAsset weaponAsset;

    private int damage;
    private int loader;
    private int ammunitions;
    private int speed;

    public WeaponAsset WeaponAsset { get => weaponAsset; set => weaponAsset = value; }
    public int Damage { get => damage; set => damage = value; }
    public int Loader { get => loader; set => loader = value; }
    public int Ammunitions { get => ammunitions; set => ammunitions = value; }
    public int Speed { get => speed; set => speed = value; }

    private void Start()
    {
        damage = WeaponAsset.Damage;
        loader = WeaponAsset.Loader;
        ammunitions = WeaponAsset.Ammunitions;
        speed = WeaponAsset.Speed;
    }

    public void ResetAsset()
    {
        Damage = damage;
        Loader = loader;
        Ammunitions = ammunitions;
        Speed = speed;
    }
}
