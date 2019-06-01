using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] private WeaponAsset weaponAsset;

    public WeaponAsset WeaponAsset { get => weaponAsset; set => weaponAsset = value; }
}
