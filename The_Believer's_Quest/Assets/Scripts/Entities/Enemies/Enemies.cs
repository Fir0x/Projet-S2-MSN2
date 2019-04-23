using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Enemies : MovingObject
{
    [SerializeField] private EnemyAsset enemyAsset;

    private int HP;
    
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
         
    }
}