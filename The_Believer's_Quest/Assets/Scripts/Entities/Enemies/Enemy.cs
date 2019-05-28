using UnityEngine;
using System;
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

    public EnemyAsset EnemyAsset { get => enemyAsset; set => enemyAsset = value; }

    void Start()
    {
        HP = enemyAsset.Hp;
        
        realPathfinding = GetComponentInParent<RealPathfinding>();
        aerialPathfinding = GetComponentInParent<AerialPathfinding>();

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
    }

    delegate void ChoosePathfinding();

    private void AStarPathfindingMoving()
    {
        startPos = this.transform.position;
        
        nextNode = realPathfinding.FindPath(startPos, transformPlayer.position);

        if (nextNode != null)
        {
            transform.position = Vector3.MoveTowards(startPos, nextNode.worldPos + new Vector3(0.5f, 0.5f, 0), EnemyAsset.Speed * Time.deltaTime);
        }
        else if (transform.position.magnitude - transformPlayer.position.magnitude < 0.5)
        {
            transform.position = Vector2.MoveTowards(transform.position, transformPlayer.position, enemyAsset.Speed * Time.deltaTime);
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

    public void SetHP(int damage)
    {
        HP -= damage;
        if (HP < 0 )
            Destroy(gameObject);
    }
}