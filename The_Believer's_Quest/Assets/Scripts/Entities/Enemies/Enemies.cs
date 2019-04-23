using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Enemies : MovingObject
{
    [SerializeField] private EnemyAsset asset;
    public EnemyAsset enemyAsset
    {
        get => asset;
        set => asset = value;
    }

    private int HP;
    
    void Start()
    {
        HP = enemyAsset.Hp;
    }
    
    public int GetWeight()
    {
        return enemyAsset.Weight;
    }
}