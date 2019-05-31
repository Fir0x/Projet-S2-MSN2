using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//Nicolas I
public class LayerFront : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    public Tilemap Tilemap { get => tilemap; set => tilemap = value; }

    public void SetTiles(List<Board.DoorPos> doors, TileAsset tiles, int floor, bool isBoss, int bossDoor)
    {
        if (isBoss)
        {
            if (doors.Contains(Board.DoorPos.Up))
                tilemap.SetTile(new Vector3Int(0, 6, 0), tiles.Tiles[1 + (floor - 1) * 5 + 11]);

            if (doors.Contains(Board.DoorPos.Right))
                tilemap.SetTile(new Vector3Int(8, 1, 0), tiles.Tiles[2 + (floor - 1) * 5 + 11]);

            if (doors.Contains(Board.DoorPos.Left))
                tilemap.SetTile(new Vector3Int(-8, 1, 0), tiles.Tiles[7 + (floor - 1) * 5 + 9]);
        }
        else
        {
            if (bossDoor == -1)
            {
                if (doors.Contains(Board.DoorPos.Up))
                    tilemap.SetTile(new Vector3Int(0, 6, 0), tiles.Tiles[1 + (floor - 1) * 5]);

                if (doors.Contains(Board.DoorPos.Right))
                    tilemap.SetTile(new Vector3Int(8, 1, 0), tiles.Tiles[2 + (floor - 1) * 5]);

                if (doors.Contains(Board.DoorPos.Left))
                    tilemap.SetTile(new Vector3Int(-8, 1, 0), tiles.Tiles[7 + (floor - 1) * 5]);
            }
            else
            {
                if (bossDoor == 1)
                    tilemap.SetTile(new Vector3Int(0, 6, 0), tiles.Tiles[1 + (floor - 1) * 5 + 11]);
                else if (doors.Contains(Board.DoorPos.Up))
                    tilemap.SetTile(new Vector3Int(0, 6, 0), tiles.Tiles[1 + (floor - 1) * 5]);

                if (bossDoor == 2)
                    tilemap.SetTile(new Vector3Int(8, 1, 0), tiles.Tiles[2 + (floor - 1) * 5 + 11]);
                else if (doors.Contains(Board.DoorPos.Right))
                    tilemap.SetTile(new Vector3Int(8, 1, 0), tiles.Tiles[2 + (floor - 1) * 5]);

                if (bossDoor == 0)
                    tilemap.SetTile(new Vector3Int(-8, 1, 0), tiles.Tiles[7 + (floor - 1) * 5 + 9]);
                else if (doors.Contains(Board.DoorPos.Left))
                    tilemap.SetTile(new Vector3Int(-8, 1, 0), tiles.Tiles[7 + (floor - 1) * 5]);
            }
            
        }
    }

    public void ClearTiles(List<Board.DoorPos> doors, TileAsset tiles, int floor)
    {
        if (doors.Contains(Board.DoorPos.Up))
            tilemap.SetTile(new Vector3Int(0, 6, 0), null);

        if (doors.Contains(Board.DoorPos.Right))
            tilemap.SetTile(new Vector3Int(8, 1, 0), tiles.Tiles[4 + (floor - 1) * 5]);

        if (doors.Contains(Board.DoorPos.Left))
            tilemap.SetTile(new Vector3Int(-8, 1, 0), tiles.Tiles[9 + (floor - 1) * 5]);
    }
}
