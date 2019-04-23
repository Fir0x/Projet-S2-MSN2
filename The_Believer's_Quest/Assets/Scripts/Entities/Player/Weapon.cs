using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Weapon : MonoBehaviour
{
    private WeaponAsset weapon;
    [SerializeField] protected PlayerAsset playerAsset;
    public WeaponAsset GetAsset()
    {
        return weapon;
    }

    public void SetWeapon(WeaponAsset weapon)
    {
        this.weapon = weapon;
    }

    public int GetDamage()
    {
        return weapon.Damage;
    }

    public int GetAmmunitions()
    {
        return weapon.Ammunitions;
    }

    public void SetAmmunitions(int ammunitions)
    {
        weapon.Ammunitions = ammunitions;
    }

    public void AddAmmunitions(int ammo)
    {
        weapon.Ammunitions = (weapon.Ammunitions + ammo) % weapon.MaxAmmunitions;
    }

    public int GetLoaderAmmo()
    {
        return weapon.Loader;
    }

    IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(weapon.ReloadingTime);
    }

    public void Reload()
    {
        weapon.Loader = (weapon.Loader + weapon.Ammunitions) % weapon.LoaderCappacity;
        print("Reload starts"); //DEBUG
        StartCoroutine(ReloadTimer());
        print("Reload ends"); //DEBUG
    }

    public void Shot () //réalisé par Sarah
    {
        weapon.Ammunitions--;
        if (weapon.Cqc)
            Attack.Cqc();
        if (weapon.Railgun) //attaque en ligne avec RailGun
            Attack.Launcher(Attack.Trajectory.Line, gameObject.GetComponent<SpriteRenderer>().sprite, playerAsset.Position, Input.mousePosition  ,weapon.Speed,weapon.Damage); 
        if (weapon.Shotgun) //attaque en Arc avec Shotgun
            Attack.Launcher(Attack.Trajectory.Arc, gameObject.GetComponent<SpriteRenderer>().sprite, playerAsset.Position, Input.mousePosition ,weapon.Speed,weapon.Damage); 
        //attaque en cercle avec Circleshot
        Attack.Launcher(Attack.Trajectory.Circle, gameObject.GetComponent<SpriteRenderer>().sprite, playerAsset.Position, Input.mousePosition  ,weapon.Speed,weapon.Damage); 
    }
}
