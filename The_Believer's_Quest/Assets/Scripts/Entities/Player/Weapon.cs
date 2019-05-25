using System;
using System.Collections;
using UnityEngine;
//Nicolas I et Sarah
[Serializable]
public class Weapon : MonoBehaviour
{
    private WeaponAsset weapon;
    private PlayerAsset playerAsset;
    public bool shot;
    private float timeBTWshots;

    private UIController UIController;

    [SerializeField] private GameObject projectile;

    public GameObject Projectile { get => projectile; set => projectile = value; }

    public void Init(UIController UIController, WeaponAsset weapon, PlayerAsset playerAsset)
    {
        this.UIController = UIController;
        this.weapon = weapon;
        this.playerAsset = playerAsset;
    }

    public WeaponAsset GetAsset()
    {
        return weapon;
    }

    public void SetWeapon(WeaponAsset weapon)
    {
        this.weapon = weapon;
        UIController.changeWeapon.Invoke();
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
        UIController.changeAmmo.Invoke();
    }

    public void AddAmmunitions(int ammo)
    {
        weapon.Ammunitions = (weapon.Ammunitions + ammo) % weapon.MaxAmmunitions;
        UIController.changeAmmo.Invoke();
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
        if (weapon.LoaderCappacity != 0)
        {
            weapon.Loader = (weapon.Loader + weapon.Ammunitions) % weapon.LoaderCappacity;
            StartCoroutine(ReloadTimer());
            UIController.changeAmmo.Invoke();
        }
    }

    private void FixedUpdate() //tourne l'arme dans le bon sens
    {
        Vector3 angle = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotz = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        if (!weapon.Cqc)
        {
            transform.rotation = Quaternion.Euler(0f,0f,rotz);
        }
        if (shot && timeBTWshots <= 0)
        {
            Shot();
            shot = false;
            timeBTWshots = 0.5f;//temps entre les tirs
        }
        else
        {
            timeBTWshots-=Time.deltaTime;
        }
    }
    //réalisé par Sarah à partir d'ici
    
    public void Shot () 
    {
        if (!weapon.Cqc)
        {
            weapon.Loader -= weapon.Nbbulletsbyshot;
            UIController.changeAmmo.Invoke();
        }
        if (weapon.Cqc) //attaque corps à corps
            Instantiate(Projectile, transform.position, transform.rotation).GetComponent<Projectile>().Init(Projectile.GetComponent<SpriteRenderer>().sprite, 10, 5, transform.right, 0f);

        if (weapon.Railgun) //attaque en ligne avec RailGun
            LineShot(playerAsset.Position,Camera.main.ScreenToWorldPoint(Input.mousePosition), weapon.Speed,weapon.Damage, 0);
        if (weapon.Shotgun) //attaque en Arc avec Shotgun
            ArcShot( weapon.Nbbulletsbyshot ,playerAsset.Position,Camera.main.ScreenToWorldPoint(Input.mousePosition), weapon.Speed,weapon.Damage);
        //attaque en cercle avec Circleshot
        if (weapon.Circleshot)
            CircleShot(weapon.Nbbulletsbyshot,playerAsset.Position,Camera.main.ScreenToWorldPoint(Input.mousePosition), weapon.Speed,weapon.Damage);
   
    }
    private void LineShot( Vector3 origin, Vector3 direction, float speed, int damage, float angle)//tir linéaire
    {
        Instantiate(Projectile, transform.position, Quaternion.identity).GetComponent<Projectile>().Init(Projectile.GetComponent<SpriteRenderer>().sprite, weapon.Speed, weapon.Damage, direction, angle); 
    }
    
    private void CircleShot(int nbprojectile, Vector3 origin, Vector3 direction, float speed, int damage)//tir en cercle
    {
        
        float angle = (float) (2*Math.PI / nbprojectile);
        
        for (int i = 0; i < nbprojectile; i++)
        {
            LineShot(origin,direction,weapon.Speed,weapon.Damage, angle *i);
        } 
    }
    private  void ArcShot(int nbprojectile, Vector3 origin, Vector3 direction, float speed, int damage) //tir type shotgun
    {
        double angle;
        Quaternion rotation = transform.rotation;

        if (nbprojectile % 2 != 0)
        {
            LineShot(origin,direction,weapon.Speed,weapon.Damage, 0);
        }

        for (int i = 1; i <= nbprojectile; i++)
        {
            LineShot(origin,direction,weapon.Speed,weapon.Damage, 20*i);
            LineShot(origin,direction,weapon.Speed,weapon.Damage, -20 *i);
        } 
        
    }



}