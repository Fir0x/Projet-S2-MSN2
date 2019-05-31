using System;
using System.Collections;
using UnityEngine;
//Nicolas I et Sarah
[Serializable]
public class Weapon : MonoBehaviour
{
    private WeaponAsset weapon;
    private PlayerAsset playerAsset;
    private bool shot;

    [SerializeField] private GameObject projectile;

    public GameObject Projectile { get => projectile; set => projectile = value; }
    public WeaponAsset.WeaponType Type { get => weapon.Type; }

    public void Init(WeaponAsset weapon, PlayerAsset playerAsset)
    {
        shot = true;
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
        UIController.uIController.changeWeapon.Invoke();
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
        UIController.uIController.changeAmmo.Invoke();
    }

    public void AddAmmunitions(int ammo)
    {
        weapon.Ammunitions = (weapon.Ammunitions + ammo) % weapon.MaxAmmunitions;
        UIController.uIController.changeAmmo.Invoke();
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
            UIController.uIController.changeAmmo.Invoke();
        }
    }

    IEnumerator CoolDown()
    {
        Shot();
        shot = false;
        yield return new WaitForSeconds(weapon.Cooldown);
        shot = true;
    }

    public void Attack() //tourne l'arme dans le bon sens
    {
        if (shot)
        {
            Vector3 angle = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotz = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
            if (!(weapon.Type == WeaponAsset.WeaponType.CQC))
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotz-90);

            }
            StartCoroutine(CoolDown());
        }
    }
    //réalisé par Sarah à partir d'ici
    
    public void Shot() 
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (!(weapon.Type == WeaponAsset.WeaponType.CQC))
        {
            weapon.Loader -= weapon.Nbbulletsbyshot;
            UIController.uIController.changeAmmo.Invoke();
        }

        if ((weapon.Type == WeaponAsset.WeaponType.CQC)) 
            //attaque corps à corps
        {
            Cqc();
        }

        if ((weapon.Type == WeaponAsset.WeaponType.Line))
            //attaque en ligne avec RailGun
            LineShot(transform.up, weapon.Speed,weapon.Damage, 0);
        if ((weapon.Type == WeaponAsset.WeaponType.Shotgun)) 
            //attaque en Arc avec Shotgun
            ArcShot(weapon.Nbbulletsbyshot, transform.up, weapon.Speed, weapon.Damage);
        //attaque en cercle avec Circleshot
        if ((weapon.Type == WeaponAsset.WeaponType.Circle))
            CircleShot(weapon.Nbbulletsbyshot, weapon.Speed, weapon.Damage);

   
    }

    private void Cqc()
    {
        Collider2D[] enemiesTouched =
            Physics2D.OverlapCircleAll(transform.position, 0.5f, LayerMask.GetMask("Aerial", "Ground"));
        for(int i = 0; i < enemiesTouched.Length; i++)
        {
            enemiesTouched[i].gameObject.GetComponent<Enemy>().TakeDamage(weapon.Damage);
        }

    }
    private void LineShot(Vector3 direction, float speed, int damage, float angle)//tir linéaire
    {
        Instantiate(projectile, transform.position,
            transform.rotation).GetComponent<Projectile>().Init(Projectile.GetComponent<SpriteRenderer>().sprite, speed, damage, transform.position, angle, direction, true); 
    }
    
    private void CircleShot(int nbprojectile, float speed, int damage)//tir en cercle
    {
        
        float angle = 360 / nbprojectile;
        
        for (int i = 0; i < nbprojectile; i++)
        {
            LineShot(new Vector3(1, 0, transform.position.z), weapon.Speed,weapon.Damage, angle *i);
        } 
    }
    private  void ArcShot(int nbprojectile, Vector3 direction, float speed, int damage) //tir type shotgun
    {
        if (nbprojectile % 2 != 0)
        {
            LineShot(direction, weapon.Speed, weapon.Damage, 0);
        }

        for (int i = 1; i <= nbprojectile / 2; i++)
        {
            LineShot(direction,weapon.Speed, weapon.Damage, 20 * i);
            LineShot(direction,weapon.Speed, weapon.Damage, -20 * i);
        } 
        
    }



}