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

    private Node nextNode;
    private SlimePathfinding slimePathfinding;
    private Transform transform1;

    public EnemyAsset EnemyAsset { get => enemyAsset; set => enemyAsset = value; }

    void Start()
    {
        transform1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        slimePathfinding = this.GetComponentInParent<SlimePathfinding>();
    }

    private void FixedUpdate()
    {
        startPos = this.transform.position;
        nextNode = slimePathfinding.FindPath(startPos, transform1.position);
        print("startpos: " + startPos.x + " " + startPos.y + " nextpos: " + nextNode.gridX + " " + nextNode.gridY);
        transform.position = Vector3.MoveTowards(startPos, nextNode.worldPos + new Vector3(0.5f, 0.5f, 0), EnemyAsset.Speed * Time.deltaTime);
    }
}