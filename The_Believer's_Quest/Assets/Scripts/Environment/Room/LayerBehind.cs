using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//Nicolas I
public class LayerBehind : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    public Tilemap Tilemap { get => tilemap; set => tilemap = value; }

    public void SetTiles(List<Board.DoorPos> doors, TileAsset tiles, int floor, bool isBoss, int bossDoor)
    {
        if (isBoss)
        {
            if (doors.Contains(Board.DoorPos.Up))
                tilemap.SetTile(new Vector3Int(0, 7, 0), tiles.Tiles[(floor - 1) * 18 + 11]);

            if (doors.Contains(Board.DoorPos.Right))
                tilemap.SetTile(new Vector3Int(8, 0, 0), tiles.Tiles[3 + (floor - 1) * 18 + 11]);

            if (doors.Contains(Board.DoorPos.Down))
                tilemap.SetTile(new Vector3Int(0, -6, 0), tiles.Tiles[6 + (floor - 1) * 18 + 9]);

            if (doors.Contains(Board.DoorPos.Left))
                tilemap.SetTile(new Vector3Int(-8, 0, 0), tiles.Tiles[8 + (floor - 1) * 18 + 9]);
        }
        else
        {
            if (bossDoor == -1)
            {
                if (doors.Contains(Board.DoorPos.Up))
                    tilemap.SetTile(new Vector3Int(0, 7, 0), tiles.Tiles[(floor - 1) * 18]);

                if (doors.Contains(Board.DoorPos.Right))
                    tilemap.SetTile(new Vector3Int(8, 0, 0), tiles.Tiles[3 + (floor - 1) * 18]);

                if (doors.Contains(Board.DoorPos.Down))
                    tilemap.SetTile(new Vector3Int(0, -6, 0), tiles.Tiles[6 + (floor - 1) * 18]);

                if (doors.Contains(Board.DoorPos.Left))
                    tilemap.SetTile(new Vector3Int(-8, 0, 0), tiles.Tiles[8 + (floor - 1) * 18]);
            }
            else
            {
                if (bossDoor == 1)
                    tilemap.SetTile(new Vector3Int(0, 7, 0), tiles.Tiles[(floor - 1) * 18 + 11]);
                else if (doors.Contains(Board.DoorPos.Up))
                    tilemap.SetTile(new Vector3Int(0, 7, 0), tiles.Tiles[(floor - 1) * 18]);

                if (bossDoor == 2)
                    tilemap.SetTile(new Vector3Int(8, 0, 0), tiles.Tiles[3 + (floor - 1) * 18 + 11]);
                else if (doors.Contains(Board.DoorPos.Right))
                    tilemap.SetTile(new Vector3Int(8, 0, 0), tiles.Tiles[3 + (floor - 1) * 18]);

                if (bossDoor == 3)
                    tilemap.SetTile(new Vector3Int(0, -6, 0), tiles.Tiles[6 + (floor - 1) * 18 + 9]);
                else if (doors.Contains(Board.DoorPos.Down))
                    tilemap.SetTile(new Vector3Int(0, -6, 0), tiles.Tiles[6 + (floor - 1) * 18]);

                if (bossDoor == 0)
                    tilemap.SetTile(new Vector3Int(-8, 0, 0), tiles.Tiles[8 + (floor - 1) * 18 + 9]);
                else if (doors.Contains(Board.DoorPos.Left))
                    tilemap.SetTile(new Vector3Int(-8, 0, 0), tiles.Tiles[8 + (floor - 1) * 18]);
            }
        }
    }

    public void ClearTiles(List<Board.DoorPos> doors, TileAsset tiles, int floor)
    {
        if (floor == 4)
        {
            floor = Random.Range(1, 3);
        }

        if (doors.Contains(Board.DoorPos.Up))
        {
            tilemap.SetTile(new Vector3Int(0, 7, 0), null);
        }

        if (doors.Contains(Board.DoorPos.Right))
        {
            tilemap.SetTile(new Vector3Int(8, 0, 0), tiles.Tiles[5 + (floor - 1) * 18]);
        }

        if (doors.Contains(Board.DoorPos.Down))
        {
            tilemap.SetTile(new Vector3Int(0, -6, 0), null);
        }

        if (doors.Contains(Board.DoorPos.Left))
        {
            tilemap.SetTile(new Vector3Int(-8, 0, 0), tiles.Tiles[10 + (floor - 1) * 18]);
        }
    }
}
