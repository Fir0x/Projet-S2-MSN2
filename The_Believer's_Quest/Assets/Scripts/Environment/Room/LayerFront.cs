using UnityEngine;
using UnityEngine.Tilemaps;
//Nicolas I
public class LayerFront : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    public Tilemap Tilemap { get => tilemap; set => tilemap = value; }

    public void SetTile(TileBase doorTile)
    {
        tilemap.SetTile(new Vector3Int(0, 6, 0), doorTile);
    }

    public void ClearTile()
    {
        tilemap.SetTile(new Vector3Int(0, 6, 0), null);
    }
}
