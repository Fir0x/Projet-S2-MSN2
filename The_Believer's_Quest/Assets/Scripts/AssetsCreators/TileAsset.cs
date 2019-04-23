using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//Nicolas I
[CreateAssetMenu (fileName = "TileAsset", menuName = "TileContainer")]
public class TileAsset : ScriptableObject
{
    [SerializeField] private TileBase[] tile;

    public TileBase[] Tile { get => tile; set => tile = value; }
}
