using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicolas L
[CreateAssetMenu(fileName = "NewPatternAsset", menuName = "FloorPattern")]
public class PatternAsset : ScriptableObject
{
    [SerializeField] private GameObject[] pattern;

    public GameObject[] Pattern { get => pattern; set => pattern = value; }
}
