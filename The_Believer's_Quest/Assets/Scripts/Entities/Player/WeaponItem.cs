using UnityEngine;
//Sarah
public class WeaponItem : MonoBehaviour
{
    [SerializeField] private WeaponAsset weaponAsset;

    public WeaponAsset WeaponAsset { get => weaponAsset; set => weaponAsset = value; }

    private int damage;
    private int loader;
    private int ammo;
    private int speed;


    private void Start()
    {
        damage = WeaponAsset.Damage;
        loader = WeaponAsset.Loader;
        ammo = WeaponAsset.Ammunitions;
        speed = WeaponAsset.Speed;
    }
    
    public void ResetAsset()
    {
        WeaponAsset.Damage = damage;
        WeaponAsset.Loader = loader;
        WeaponAsset.Ammunitions = ammo;
        WeaponAsset.Speed = speed;
    }
}
