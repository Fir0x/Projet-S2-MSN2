using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
//Nicolas I et Sarah
[Serializable]
public class Weapon : MonoBehaviour
{
    private WeaponAsset weapon;
    private PlayerAsset playerAsset;
    public bool shot;

    public GameObject projectile;
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
        /*if (weapon.Cqc)
        {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else*/
           transform.rotation = Quaternion.Euler(0f,0f,rotz);

        if (shot)
        {
            Shot();
            shot = false;
        }
    }
    //réalisé par Sarah à partir d'ici
    
    public void Shot () 
    {
        if (!weapon.Cqc)
            weapon.Ammunitions--;
        
        if (weapon.Cqc) //essai attaque corps à corps
        {
            print("before");
            Instantiate(projectile, transform.position, transform.rotation).GetComponent<Projectile>().Init(projectile.GetComponent<SpriteRenderer>().sprite, 10, 5, transform.right);
            print("after");
        }
        if (weapon.Railgun) //attaque en ligne avec RailGun
            LineShot(playerAsset.Position,Camera.main.ScreenToWorldPoint(Input.mousePosition), weapon.Speed,weapon.Damage);
        if (weapon.Shotgun) //attaque en Arc avec Shotgun
            ArcShot( weapon.Nbbulletsbyshot ,playerAsset.Position,Camera.main.ScreenToWorldPoint(Input.mousePosition), weapon.Speed,weapon.Damage);
        //attaque en cercle avec Circleshot
        if (weapon.Circleshot)
            CircleShot(weapon.Nbbulletsbyshot,playerAsset.Position,Camera.main.ScreenToWorldPoint(Input.mousePosition), weapon.Speed,weapon.Damage);
   
    }
    private void LineShot( Vector3 origin, Vector3 direction, float speed, int damage)//tir linéaire
    {
        GameObject newproj = Instantiate(projectile, transform.position, transform.rotation);
        newproj.GetComponent<Projectile>().Init(projectile.GetComponent<SpriteRenderer>().sprite, speed, damage, transform.right);
    }

    private void CircleShot(int nbprojectile, Vector3 origin, Vector3 direction, float speed, int damage)//tir en cercle
    {
        
        double angle = 360 / nbprojectile;
        for (int i = 0; i < nbprojectile; i++)
        {
            GameObject newproj = Instantiate(projectile, transform.position, transform.rotation);
            newproj.GetComponent<Projectile>().Init(projectile.GetComponent<SpriteRenderer>().sprite, speed, damage, transform.right);
            direction = new Vector3(direction.x, (int) (direction.y * Math.Sin(angle)), direction.z);
        } 
    }
    private  void ArcShot(int nbprojectile, Vector3 origin, Vector3 direction, float speed, int damage) //tir type shotgun
    {
        GameObject newproj = Instantiate(projectile, transform.position, transform.rotation);
        newproj.GetComponent<Projectile>().Init(projectile.GetComponent<SpriteRenderer>().sprite, speed, damage, transform.right);

        double angle = 120 / nbprojectile;
        
        for (int i = 1; i < nbprojectile/2; i++)
        {
            direction = new Vector3(direction.x, (int) (direction.y * Math.Sin(angle)), direction.z);
            newproj = Instantiate(projectile, transform.position, transform.rotation);
            newproj.GetComponent<Projectile>().Init(projectile.GetComponent<SpriteRenderer>().sprite, speed, damage, transform.right);

        } 
        
        for (int i = 1; i < nbprojectile/2; i++)
        {
            
            direction = new Vector3(direction.x, (int) (direction.y * Math.Sin(-angle)), direction.z);
            newproj = Instantiate(projectile, transform.position, transform.rotation);
            newproj.GetComponent<Projectile>().Init(projectile.GetComponent<SpriteRenderer>().sprite, speed, damage, transform.right);
        } 
    }



}