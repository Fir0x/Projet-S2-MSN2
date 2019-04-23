using UnityEngine;
//Nicolas I
[CreateAssetMenu(fileName = "NewPatternAsset", menuName = "FloorPattern")]
public class PatternAsset : ScriptableObject
{
    [SerializeField] private GameObject[] pattern;

    public GameObject[] Pattern { get => pattern; set => pattern = value; }
}
