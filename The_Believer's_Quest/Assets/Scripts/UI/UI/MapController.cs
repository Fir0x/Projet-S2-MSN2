using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private Tilemap cursorMask;
    [SerializeField] private TileAsset tiles;
    [SerializeField] private Camera cam;

    private int[,] board;
    private int width;
    private int height;

    private int mapRoomWidth = 3;
    private int mapRoomHeight = 3;

    private HashSet<int[]> visitedRoom;

    public Tilemap Map { get => map; set => map = value; }
    public Tilemap CursorMask { get => cursorMask; set => cursorMask = value; }
    public TileAsset Tiles { get => tiles; set => tiles = value; }
    public Camera Cam { get => cam; set => cam = value; }

    public void InitMap(int[,] board, int width, int height)
    {
        this.board = board;
        this.width = width;
        this.height = height;
    }

    public void EnterRoom(int[] pos)
    {
        if (visitedRoom.Contains(pos))
            DrawNew(pos);

        cursorMask.SetTile(new Vector3Int(pos[0] * 3, pos[1] * 3, 0), tiles.Tiles[0]);
    }

    private void DrawNew(int[] pos)
    {
        int midWidth = mapRoomWidth / 2;
        int midHeight = mapRoomHeight / 2;
        for (int i = pos[0] - midWidth; i < pos[0] + mapRoomWidth - midWidth; i++)
        {
            for (int j = pos[1] - midHeight; j < pos[1] + mapRoomHeight - midHeight; j++)
                map.SetTile(new Vector3Int(i, j, 0), tiles.Tiles[1]);
        }
    }
}