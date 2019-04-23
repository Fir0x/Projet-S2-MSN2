using UnityEngine;
using System;
using System.Collections.Generic;
using Entities;
using UnityEditor;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Enemies : MovingObject
{
    [SerializeField] private EnemyAsset enemyAsset;

    private Vector3 startPos;
    private int HP;

    private Node nextNode;
    private SlimePathfinding slimePathfinding;
    private Transform transform1;

    void Start()
    {
        transform1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        slimePathfinding = this.GetComponentInParent<SlimePathfinding>();
        startPos = this.transform.position;
    }

    private void FixedUpdate()
    {
        nextNode = slimePathfinding.FindPath(this.transform.position, transform1.position);
        transform.position = Vector2.MoveTowards(startPos, nextNode.worldPos, enemyAsset.Speed * Time.deltaTime);
    }
}