using UnityEngine;
//Sarah
public class WeaponItem : MonoBehaviour
{
    [SerializeField] private WeaponAsset weaponAsset;

    public WeaponAsset WeaponAsset { get => weaponAsset; set => weaponAsset = value; }
}