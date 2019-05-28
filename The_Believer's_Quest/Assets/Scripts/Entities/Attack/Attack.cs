using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject projectile;
    public PlayerAsset player;
    public EnemyAsset enemy;

    
    public enum Trajectory
    {
        Line,
        Arc,
        Railgun,
        Circle,
        Cqc,
    }


    public void Launcher()
    {
        if (enemy._Trajectory == Trajectory.Line)
            LineShot(player.Position, enemy.Speed, enemy.Damage, 0f);
        if (enemy._Trajectory== Trajectory.Arc)
            ArcShot(enemy.NbOfProjectiles, player.Position, enemy.Speed, enemy.Damage);
        if (enemy._Trajectory== Trajectory.Circle)
            CircleShot(enemy.NbOfProjectiles, enemy.Speed, enemy.Damage);
        if (enemy._Trajectory == Trajectory.Cqc)
            Cqc(enemy.Damage);
        if (enemy._Trajectory == Trajectory.Railgun)
            Railgun(enemy.Damage);
    }
    private void Cqc(int damage)
    {
        Collider2D[] playerTouched =
            Physics2D.OverlapCircleAll(transform.position, 1, LayerMask.GetMask("Default"));
        for(int i = 0; i < playerTouched.Length; i++)
        {
            if (playerTouched[i].CompareTag("Player"))
                playerTouched[i].GetComponent<Player>().SetLife(player.Hp - damage);
        }
    }
    private void LineShot(Vector3 direction, float speed, int damage, float angle)//tir linéaire
    {
        Instantiate(projectile, gameObject.transform.position,
            Quaternion.Euler(0f,0f,0f)).GetComponent<Projectile>().Init(enemy.Sprite, speed, damage, gameObject.transform.position, angle, direction,false); 
    }
    
    private void CircleShot(int nbprojectile, float speed, int damage)//tir en cercle
    {
        
        float angle = 360 / nbprojectile;
        
        for (int i = 0; i < nbprojectile; i++)
        {
            LineShot(new Vector3(1, 0, transform.position.z), enemy.Speed, damage, angle *i);
        } 
    }

    private void ArcShot(int nbprojectile, Vector3 direction, float speed, int damage) //tir type shotgun
    {
        if (nbprojectile % 2 != 0)
        {
            LineShot(direction, enemy.Speed, damage, 0);
        }

        for (int i = 1; i <= nbprojectile / 2; i++)
        {
            LineShot(direction, speed, damage, 20 * i);
            LineShot(direction, speed, damage, -20 * i);
        }
    }

    private void Railgun(int damage)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(gameObject.transform.position, player.Position, 20, LayerMask.GetMask("Default"));
   
        if (hitInfo.collider != null )
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                hitInfo.collider.GetComponent<Player>().SetLife(player.Hp - damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("Pattern"))
            {
                Destroy(gameObject);
            }
        }
    }

}
