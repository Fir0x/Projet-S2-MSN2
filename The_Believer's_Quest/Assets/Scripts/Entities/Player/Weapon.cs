using System;
using System.Collections;
using UnityEngine;
//Nicolas I et Sarah
[Serializable]
public class Weapon : MonoBehaviour
{
    public GameObject weaponGO;
    private WeaponItem dynamicData;
    private WeaponAsset weapon;
    private PlayerAsset playerAsset;
    private bool shot;

    [SerializeField] private GameObject projectile;

    public GameObject Projectile { get => projectile; set => projectile = value; }
    public WeaponAsset.WeaponType Type { get => weapon.Type; }

    public void Init(GameObject weapon, PlayerAsset playerAsset)
    {
        shot = true;
        dynamicData = weapon.GetComponent<WeaponItem>();
        this.weapon = dynamicData.WeaponAsset;
        this.playerAsset = playerAsset;
        GetComponent<SpriteRenderer>().sprite = this.weapon.Sprite;
    }

    private void FixedUpdate()
    {
        if (GameObject.Find("Player").GetComponent<Player>().PlayerAsset.WeaponsList[0] != null)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Player").GetComponent<Player>().PlayerAsset.WeaponsList[0].GetComponent<WeaponItem>().WeaponAsset.Sprite;
        }
        else if (GameObject.Find("Player").GetComponent<Player>().PlayerAsset.WeaponsList[0] != null)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Player").GetComponent<Player>().PlayerAsset.WeaponsList[1].GetComponent<WeaponItem>().WeaponAsset.Sprite;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public WeaponAsset GetAsset()
    {
        return weapon;
    }

    public void SetWeapon(GameObject weapon)
    {
        if(weapon != null)
        {
            dynamicData = weapon.GetComponent<WeaponItem>();
            this.weapon = dynamicData.WeaponAsset;
            GameObject temp;
            temp = playerAsset.WeaponsList[1];
            playerAsset.WeaponsList[1] = playerAsset.WeaponsList[0];
            playerAsset.WeaponsList[0] = temp;
            temp = playerAsset.weaponsInstance[1];
            playerAsset.weaponsInstance[1] = playerAsset.weaponsInstance[0];
            playerAsset.weaponsInstance[0] = temp;

            UIController.uIController.changeWeapon.Invoke();
            UIController.uIController.changeAmmo.Invoke();
            weaponGO.GetComponent<SpriteRenderer>().sprite = weapon.GetComponent<WeaponItem>().WeaponAsset.Sprite;
        }
    }
        
    public void MultiplyDamage(float dmg)
    {
        dynamicData.Damage *= (int) dmg;
    }
    public int GetDamage()
    {
        return dynamicData.Damage;
    }

    public int GetAmmunitions()
    {
        return dynamicData.Ammunitions;
    }

    public void SetAmmunitions(int ammunitions)
    {
        dynamicData.Ammunitions = ammunitions;
        UIController.uIController.changeAmmo.Invoke();
    }

    public void AddAmmunitions(int ammo)
    {
        dynamicData.Ammunitions = (weapon.Ammunitions + ammo) % weapon.MaxAmmunitions;
        UIController.uIController.changeAmmo.Invoke();
    }

    public int GetLoaderAmmo()
    {
        return dynamicData.Loader;
    }
    
    IEnumerator ReloadTimer()
    {
        shot = false;
        Reload();
        yield return new WaitForSeconds(weapon.ReloadingTime);
        if (dynamicData.Loader > 0)
            shot = true;
    }

    public void Reload()
    {
        if ( dynamicData.Ammunitions > 0)
        {
            if (weapon.LoaderCappacity != 0 && dynamicData.Loader > 0)
            {
                int bullets = dynamicData.Loader;
                dynamicData.Loader = weapon.LoaderCappacity;
                dynamicData.Ammunitions -= weapon.LoaderCappacity + bullets;
            }
            else
            {
                if (dynamicData.Ammunitions < weapon.LoaderCappacity)
                {
                    dynamicData.Loader = dynamicData.Ammunitions;
                    dynamicData.Ammunitions = 0;
                }
                else
                {
                    dynamicData.Loader = weapon.LoaderCappacity;
                    dynamicData.Ammunitions -= weapon.LoaderCappacity;
                }
            }
        }

    }

    IEnumerator CoolDown()
    {
        if (weapon.Type == WeaponAsset.WeaponType.CQC ||(dynamicData.Ammunitions >= 0 && dynamicData.Loader > 0))
        {
            Shot();
            shot = false;
            yield return new WaitForSeconds(weapon.Cooldown);
            shot = true;
        }
        else
            shot = false;
    }

    public void Attack() //tourne l'arme dans le bon sens
    {
        print("attack");
        if (shot)
        {
            print("attack2");
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
        if (weapon.Type != WeaponAsset.WeaponType.CQC)
        {
            if (dynamicData.Loader <= 0)//teste si on a encore des balles
                StartCoroutine(ReloadTimer());
            dynamicData.Loader--;
            print(UIController.uIController);
            UIController.uIController.changeAmmo.Invoke();

            if ((weapon.Type == WeaponAsset.WeaponType.Line))
                //attaque en ligne avec RailGun
                LineShot(dynamicData.Speed, dynamicData.Damage, 0);
            if ((weapon.Type == WeaponAsset.WeaponType.Shotgun))
                //attaque en Arc avec Shotgun
                ArcShot(weapon.Nbbulletsbyshot);
            if ((weapon.Type == WeaponAsset.WeaponType.Circle))
                //attaque en cercle avec Circleshot
                CircleShot(weapon.Nbbulletsbyshot);
        }
        //attaque corps à corps
         Cqc();
        GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().PlaySingle(playerAsset.WeaponsList[0].GetComponent<WeaponItem>().WeaponAsset.Clip);
    }

    private void Cqc()
    {
        Collider2D[] enemiesTouched =
            Physics2D.OverlapCircleAll(transform.position, 0.5f, LayerMask.GetMask("Aerial", "Ground"));
        for(int i = 0; i < enemiesTouched.Length; i++)
        {
            if (enemiesTouched[i].CompareTag("Enemy"))
                enemiesTouched[i].gameObject.GetComponent<Enemy>().TakeDamage(dynamicData.Damage);
            if (enemiesTouched[i].CompareTag("Boss"))
                enemiesTouched[i].gameObject.GetComponent<Boss>().ChangeLife(-dynamicData.Damage);
        }

    }
    private void LineShot( float speed, int damage, float angle)//tir linéaire
    {
        Instantiate(projectile, transform.position,
            transform.rotation).GetComponent<Projectile>().Init(weapon.Bullet, speed, damage, transform.position, angle, true); 
    }
    
    private void CircleShot(int nbprojectile)//tir en cercle
    {
        
        float angle = 360 / nbprojectile;
        
        for (int i = 0; i < nbprojectile; i++)
        {

            LineShot(dynamicData.Speed, dynamicData.Damage, angle *i);
        } 
    }
    private  void ArcShot(int nbprojectile) //tir type shotgun
    {
        if (nbprojectile % 2 != 0)
        {
            LineShot(dynamicData.Speed, dynamicData.Damage, 0);
        }

        for (int i = 1; i <= nbprojectile / 2; i++)
        {
            LineShot(dynamicData.Speed, dynamicData.Damage, 10 * i);
            LineShot(dynamicData.Speed, dynamicData.Damage, -10 * i);
        } 
        
    }


}