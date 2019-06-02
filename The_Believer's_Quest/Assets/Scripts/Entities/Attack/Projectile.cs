﻿using UnityEngine;
public class Projectile : MovingObject
{
    [SerializeField] protected PlayerAsset playerAsset;
    private float speed;
    private float damage;
    private Vector3 direction;
    private bool player;
   
    
    //Sarah

    public void Init(Sprite sprite, float speed, float damage, Vector3 origin, float angle, Vector3 _direction, bool player, bool circle) 
    {
        //projectiles ennemis
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.speed = speed;
        this.damage = damage;
        this.player = player;
        if (circle)
            _direction = transform.up;
        
        direction = _direction;
        if (angle != 0)
            transform.RotateAround(origin, Vector3.forward, angle);

    }
    public void Init(Sprite sprite, float speed, float damage, Vector3 origin, float angle, bool player) 
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.speed = speed;
        this.damage = damage;
        this.player = player;
        if (angle != 0)
            transform.RotateAround(origin, Vector3.forward, angle);

    }
    private void FixedUpdate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, 0.1f, LayerMask.GetMask("Aerial", "Ground", "Obstacle", "Default"));
   
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy") && player)
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("Boss") && player)
            {
                hitInfo.collider.GetComponent<Boss>().ChangeLife(-damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("Player") && !player)
            {
                hitInfo.collider.GetComponent<Player>().SetLife(playerAsset.Hp - damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("Pattern"))
            {
               Destroy(gameObject);
            }
        }
        

        if (player)
            direction = transform.up;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        
    }
}
