﻿using System;
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
        Transform playerT = GameObject.FindGameObjectWithTag("Player").transform;
        if (enemy._Trajectory == Trajectory.Line)
            LineShot(playerT.position, enemy.Speed, enemy.Damage, 0f);
        if (enemy._Trajectory == Trajectory.Arc)
            ArcShot(enemy.NbOfProjectiles, playerT.position, enemy.ShotSpeed, enemy.Damage);
        if (enemy._Trajectory == Trajectory.Circle)
            CircleShot(enemy.NbOfProjectiles, enemy.ShotSpeed, enemy.Damage);
        if (enemy._Trajectory == Trajectory.Cqc)
            Cqc(enemy.Damage);
        if (enemy._Trajectory == Trajectory.Railgun)
            Railgun(enemy.Damage);
    }
    private void Cqc(int damage)
    {
        Collider2D[] playerTouched = Physics2D.OverlapCircleAll(transform.position, 0.5f, LayerMask.GetMask("Default"));

        if(!player.Invicibility)
        {
            for (int i = 0; i < playerTouched.Length; i++)
            {
                if (playerTouched[i].CompareTag("Player"))
                {
                    playerTouched[i].GetComponent<Player>().SetLife(player.Hp - damage);
                    print("touché!");
                    
                    Vector3 forcedMov = -(gameObject.transform.position - playerTouched[i].transform.position).normalized;              //to make player move in the opposite way when touched
                    playerTouched[i].GetComponent<Player>().ForcedMovement(forcedMov);

                }
            }
        }
    }
    private void LineShot(Vector3 direction, float speed, int damage, float angle)//tir linéaire
    {
       Instantiate(projectile, transform.position,
            transform.rotation).GetComponent<Projectile>().Init(enemy.Sprite, speed, damage, transform.position, angle, direction-transform.position,false); 
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
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position, 20, LayerMask.GetMask("Default"));
   
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
