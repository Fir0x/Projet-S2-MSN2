using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
//Nicolas I
[Serializable]
public class Weapon : MonoBehaviour
{
    private WeaponAsset weapon;
    private PlayerAsset playerAsset;
    
    public WeaponAsset GetAsset()
    {
        return weapon;
    }

    public void SetWeapon(WeaponAsset weapon)
    {
        this.weapon = weapon;
    }

    public void SetPlayer(PlayerAsset playerAsset)
    {
        this.playerAsset = playerAsset;
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
        print("Reload starts"); //DEBUG
        yield return new WaitForSeconds(weapon.ReloadingTime);
        print("Reload ends"); //DEBUG
    }

    public void Reload()
    {
        weapon.Loader = (weapon.Loader + weapon.Ammunitions) % weapon.LoaderCappacity;
        StartCoroutine(ReloadTimer());
    }

    private void FixedUpdate() //tourne l'arme dans le bon sens
    {
        Vector3 angle = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotz = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        if (weapon.Cqc)
        {
            if (rotz > 90 && rotz < 270)
                transform.rotation = Quaternion.Euler(0, 0f, 0f);
            else 
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
           transform.rotation = Quaternion.Euler(0f,0f,rotz -90);
        }
        
    
    }

    public void Shot () //réalisé par Sarah
    {
        print(weapon);
        weapon.Ammunitions--;
        //GameObject projectile = Instantiate(Resources.Load("Projectile") as GameObject, playerAsset.Position, new Quaternion());
        if (weapon.Cqc) //essai attaque corps à corps
        {
            
            Attack.Launcher(Attack.Trajectory.Cqc, playerAsset.Position, Input.mousePosition,(float) weapon.Speed, weapon.Damage);
        }
        /*if (weapon.Railgun) //attaque en ligne avec RailGun
            Attack.Launcher(Attack.Trajectory.Line,projectile.GetComponent<SpriteRenderer>().sprite, playerAsset.Position, Camera.main.ScreenToWorldPoint(Input.mousePosition), weapon.Speed,weapon.Damage); 
        if (weapon.Shotgun) //attaque en Arc avec Shotgun
            Attack.Launcher(Attack.Trajectory.Arc, projectile.GetComponent<SpriteRenderer>().sprite, playerAsset.Position,Camera.main.ScreenToWorldPoint(Input.mousePosition), weapon.Speed,weapon.Damage); 
        //attaque en cercle avec Circleshot
        if (weapon.Circleshot)
            Attack.Launcher(Attack.Trajectory.Circle, projectile.GetComponent<SpriteRenderer>().sprite, playerAsset.Position, Camera.main.ScreenToWorldPoint(Input.mousePosition), weapon.Speed,weapon.Damage); 
   
   */
    }
}