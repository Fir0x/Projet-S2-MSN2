using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEditor;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
//Nicolas L
public class Enemy : MovingObject
{
    [SerializeField] private EnemyAsset enemyAsset;

    private Vector3 startPos;
    private int HP;

    private LayerMask mask;
    
    private Node nextNode;
    private Transform transformPlayer;

    private ChoosePathfinding Pathfinding;
    private RealPathfinding realPathfinding;
    private AerialPathfinding aerialPathfinding;
    
    private Attack attack;
    public bool shot;
    private float timeBtwshots;

    public EnemyAsset EnemyAsset { get => enemyAsset; set => enemyAsset = value; }

    void Start()
    {
        HP = enemyAsset.Hp;
        
        realPathfinding = GetComponentInParent<RealPathfinding>();
        aerialPathfinding = GetComponentInParent<AerialPathfinding>();
        attack = GetComponent<Attack>();
        
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        transformPlayer = playerGO.GetComponent<Transform>();
        mask = gameObject.layer;

        if (mask == 9)
        {
            Pathfinding = AStarPathfindingMoving;
        }
        else
        {
            Pathfinding = SimplePathfinding;
        }

    }

    private void FixedUpdate()
    {
        Pathfinding();
        if ( shot && timeBtwshots <=0)
        {
            attack.Launcher();
            shot = false;
            timeBtwshots = enemyAsset.Cooldown;
        }
        else
        {
            timeBtwshots -= Time.deltaTime;
        }
    }
    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(enemyAsset.Cooldown);
    }
    delegate void ChoosePathfinding();

    private void AStarPathfindingMoving()
    {
        startPos = this.transform.position;
        
        nextNode = realPathfinding.FindPath(startPos, transformPlayer.position);

        if (nextNode != null)
        {
            transform.position = Vector2.MoveTowards(startPos, nextNode.worldPos + new Vector3(0.5f, 0.5f, 0), EnemyAsset.Speed * Time.deltaTime);
        }
        else
        {
            if (transform.position.magnitude - transformPlayer.position.magnitude < 0.5)
            {
                transform.position = Vector2.MoveTowards(transform.position, transformPlayer.position,
                    enemyAsset.Speed * Time.deltaTime);
            }
            shot = true;
        }
    }

    private void SimplePathfinding()
    {
        aerialPathfinding.Move(this);
    }

    public int GetWeight()
    {
        return EnemyAsset.Weight;
    }

    public void OnDestroy()
    {
        GetComponentInParent<RoomManager>().DestroyEnemy(gameObject);
    }

    public void SetHP(int hp)
    {
        HP = hp;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP < 0 )
            Destroy(gameObject);
    }
}