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
        
        transform.position = Vector3.MoveTowards(startPos, nextNode.worldPos + new Vector3(0.5f, 0.1f, 0), EnemyAsset.Speed * Time.deltaTime);
    }

    private void SimplePathfinding()
    {
        aerialPathfinding.Move(this);
    }

    public int GetWeight()
    {
        return EnemyAsset.Weight;
    }
}