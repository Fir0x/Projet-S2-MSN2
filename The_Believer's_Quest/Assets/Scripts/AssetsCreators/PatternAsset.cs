using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPatternAsset", menuName = "FloorPattern")]
public class PatternAsset : ScriptableObject
{
    [SerializeField] private GameObject[] pattern;

    public GameObject[] Pattern { get => pattern; set => pattern = value; }
}
