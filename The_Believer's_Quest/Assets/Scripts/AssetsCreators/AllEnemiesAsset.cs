using System.Collections.Generic;
using UnityEngine;

//Nicolas N
[CreateAssetMenu(fileName = "AllEnemies", menuName = "Entity/AllEnemies")]
public class AllEnemiesAsset : ScriptableObject
{
    [SerializeField] private List<GameObject> allEnemies;

    public List<GameObject> AllEnemies { get => allEnemies; set => allEnemies = value; }
}

