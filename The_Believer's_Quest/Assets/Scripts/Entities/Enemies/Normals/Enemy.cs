﻿using UnityEngine;
using System.Collections;
using Entities;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
//Nicolas L
public class Enemy : MovingObject
{
    private bool testForCoolDown;

    [SerializeField] private EnemyAsset enemyAsset;
    [SerializeField] private PlayerAsset playerAsset;

    private Vector3 startPos;
    private float HP;

    private LayerMask mask;
    
    private Node nextNode;
    private Node precedentNode;
    private Transform transformPlayer;

    private ChoosePathfinding Pathfinding;
    private RealPathfinding realPathfinding;

    private Vector3 firstPos;               //for direction, for animations
    private Vector3 nextPos;
    private Vector3 direction;

    private Attack attack;
    public bool shot;

    private EnemyAnimation animatorController;

    public EnemyAsset EnemyAsset { get => enemyAsset; set => enemyAsset = value; }

    void Start()
    {
        animatorController = GetComponent<EnemyAnimation>();
        precedentNode = null;
        firstPos = transform.position;

        testForCoolDown = true;
        HP = enemyAsset.Hp;
        
        realPathfinding = GetComponentInParent<RealPathfinding>();
        attack = GetComponent<Attack>();
        
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        transformPlayer = playerGO.GetComponent<Transform>();
        mask = gameObject.layer;

        Pathfinding = AStarPathfindingMoving;
    }

    private void FixedUpdate()
    {
        Pathfinding();
        
        if(shot)
        {
            //animatorController.ChangeAttack();
            StartCoroutine(AttackWithCoolDown());
        }
        direction = nextPos - firstPos;
        //ChangeDirection();
        firstPos = nextPos;
    }

    IEnumerator AttackWithCoolDown()
    {
        if (testForCoolDown)
        {
            testForCoolDown = false;
            attack.Launcher();
            shot = false;

            yield return new WaitForSeconds(enemyAsset.Cooldown);
            testForCoolDown = true;
        }
    }

    delegate void ChoosePathfinding();

    private void AStarPathfindingMoving()
    {
        startPos = transform.position;

        nextNode = realPathfinding.FindPath(startPos, transformPlayer.position);
        if(precedentNode != nextNode && nextNode != null)
        {
            precedentNode = nextNode;
        }

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
            else
            {
                transform.position = Vector2.MoveTowards(startPos, precedentNode.worldPos + new Vector3(0.5f, 0.5f, 0), EnemyAsset.Speed * Time.deltaTime);
            }
        }

        if (nextNode != null)
            nextPos = nextNode.worldPos;
        else if (precedentNode != null)
            nextPos = precedentNode.worldPos;
        else
            nextPos = playerAsset.Position;

        if (CanAttack())
        {
            shot = true;
        }
    }

    private bool CanAttack()
    {
        return (gameObject.transform.position - playerAsset.Position).magnitude < enemyAsset.Range;
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

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            //animatorController.Death();
            Destroy(gameObject);
        }
    }

    /*public void ChangeDirection()
    {
        float x = direction.x;
        float y = direction.y;

        if (x < 0)
            animatorController.Direction(3);
        else if (x > 0)
            animatorController.Direction(1);
        else if (y < 0)
            animatorController.Direction(2);
        else
            animatorController.Direction(0);
    }*/
}