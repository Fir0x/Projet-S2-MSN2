﻿using System.Collections.Generic;
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

    /*//Used to adapt map creation with changing value only one time
    private int mapRoomWidth = 5;
    private int mapRoomHeight = 3;*/

    private HashSet<int[]> visitedRoom;
    private int minPosX = 0;
    private int maxPosX = 0;
    private int minPosY = 0;
    private int maxPosY = 0;
    private Vector3Int playerPos;

    public static MapController mapScript; //Singleton

    public Tilemap Map { get => map; set => map = value; }
    public Tilemap CursorMask { get => cursorMask; set => cursorMask = value; }
    public TileAsset Tiles { get => tiles; set => tiles = value; }
    public Camera Cam { get => cam; set => cam = value; }

    private void Start()
    {
        mapScript = this; //Init singleton
    }

    public void InitMap(int[,] board, int width, int height)
    {
        this.board = board;
        this.width = width;
        this.height = height;
        visitedRoom = new HashSet<int[]>();
    }

    public void EnterRoom(int[] pos, string roomName, List<Board.DoorPos> doors)
    {
        if (!visitedRoom.Contains(pos))
            DrawNew(pos, roomName, doors);

        cursorMask.SetTile(playerPos, null); //Remove player cursor in previous room
        playerPos = new Vector3Int(pos[0] * 5, pos[1] * 3, 0);
        cursorMask.SetTile(playerPos, tiles.Tiles[0]); //Set player cursor in new room
    }

    private void DrawNew(int[] pos, string roomName, List<Board.DoorPos> doors)
    {
        int midWidth = 5 / 2;
        int midHeight = 3 / 2;
        for (int i = pos[0] * 5 - midWidth; i < pos[0] * 5 + 5 - midWidth; i++)
        {
            for (int j = pos[1] * 3 - midHeight; j < pos[1] * 3 + 3 - midHeight; j++)
            {
                if (i == 1 && j == 1)
                {
                    if (roomName == "Chest")
                        map.SetTile(new Vector3Int(i, j, 0), tiles.Tiles[2]);
                    else if (roomName == "Shop")
                        map.SetTile(new Vector3Int(i, j, 0), tiles.Tiles[3]);
                    else if (roomName == "Boss")
                        map.SetTile(new Vector3Int(i, j, 0), tiles.Tiles[4]);
                    else
                        map.SetTile(new Vector3Int(i, j, 0), tiles.Tiles[1]);
                }
                else
                    map.SetTile(new Vector3Int(i, j, 0), tiles.Tiles[1]);
            }
        }

        if (pos[0] < minPosX)
            minPosX = pos[0];
        else if (pos[0] > maxPosX)
            maxPosX = pos[0];

        if (pos[1] < minPosY)
            minPosY = pos[1];
        else if (pos[1] > maxPosY)
            maxPosY = pos[1];
        
        Vector3 centerTile = map.GetCellCenterWorld(new Vector3Int((maxPosX - minPosX) / 2 * 5, (maxPosY - minPosY) / 2 * 3, 0));
        cam.transform.position = centerTile; //Set rhe camera position at the center of the map
    }
}