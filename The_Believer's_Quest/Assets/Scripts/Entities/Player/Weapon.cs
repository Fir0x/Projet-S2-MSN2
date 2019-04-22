using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Weapon : MonoBehaviour
{
    private WeaponAsset weapon;

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

    public void Shot ()
    {
        weapon.Ammunitions--;
        //attaque de base pour l'instant, en ligne
        if (weapon.Cqc)
            Attack.Cqc();
        //if (weapon.Railgun) Attack.Launcher(Attack.Trajectory.Line, GameObject.FindGameObjectWithTag("Player").transform.position,  ,1.0,weapon.Damage); 
            
    }
}
