using UnityEngine;
using System;
using System.Collections.Generic;
using Entities;
using UnityEditor;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
//Nicolas L
public class Enemies : MovingObject
{
    [SerializeField] private EnemyAsset enemyAsset;

    private Vector3 startPos;
    private int HP;

    private LayerMask mask;
    
    private Node nextNode;
    private Transform transformPlayer;

    private ChoosePathfinding pathfinding;
    private RealPathfinding realPathfinding;
    private AerialPathfinding aerialPathfinding;

    public EnemyAsset EnemyAsset { get => enemyAsset; set => enemyAsset = value; }

    void Start()
    {
        realPathfinding = GetComponentInParent<RealPathfinding>();
        aerialPathfinding = GetComponentInParent<AerialPathfinding>();
        
        transformPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        mask = gameObject.layer;

        if (mask == 9)
        {
            pathfinding = AStarPathfinding;
        }
        else
        {
            pathfinding = SimplePathfinding;
        }
        
    }
    

    private void FixedUpdate()
    {
        pathfinding();
    }

    delegate void ChoosePathfinding();

    private void AStarPathfinding()
    {
        startPos = this.transform.position;
        nextNode = realPathfinding.FindPath(startPos, transformPlayer.position);
        transform.position = Vector3.MoveTowards(startPos, nextNode.worldPos + new Vector3(0.5f, 0.5f, 0), EnemyAsset.Speed * Time.deltaTime);
    }

    private void SimplePathfinding()
    {
        aerialPathfinding.Move(this);
    }
}