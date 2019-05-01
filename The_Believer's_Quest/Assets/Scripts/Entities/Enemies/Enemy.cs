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
    
    private List<Node> nextNodes;
    private KeyBoardManager keyBoard;
    private Transform transformPlayer;

    private ChoosePathfinding pathfinding;
    private RealPathfinding realPathfinding;
    private AerialPathfinding aerialPathfinding;

    public EnemyAsset EnemyAsset { get => enemyAsset; set => enemyAsset = value; }

    void Awake()
    {
        realPathfinding = GetComponentInParent<RealPathfinding>();
        aerialPathfinding = GetComponentInParent<AerialPathfinding>();

        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        keyBoard = playerGO.GetComponent<KeyBoardManager>();
        transformPlayer = playerGO.GetComponent<Transform>();
        mask = gameObject.layer;

        if (mask == 9)
        {
            keyBoard.AddToMove(AStarPathfindingStanding);
            //pathfinding = AStarPathfindingStanding;
            nextNodes = AStarPathfindingMoving();
        }
        else
        {
            pathfinding = SimplePathfinding;
        }

    }

    private void FixedUpdate()
    {
        pathfinding?.Invoke();
    }

    delegate void ChoosePathfinding();

    private List<Node> AStarPathfindingMoving()
    {
        startPos = this.transform.position;
        
        return realPathfinding.FindPath(startPos, transformPlayer.position);
    }

    private void AStarPathfindingStanding()
    {
        if (transform.position == nextNodes[0].worldPos + new Vector3(0.5f, 0.5f, 0))
        {
            nextNodes.RemoveAt(0);
        }
        transform.position = Vector3.MoveTowards(startPos, nextNodes[0].worldPos + new Vector3(0.5f, 0.5f, 0), EnemyAsset.Speed * Time.deltaTime);
    }

    private void SimplePathfinding()
    {
        aerialPathfinding.Move(this);
    }
}