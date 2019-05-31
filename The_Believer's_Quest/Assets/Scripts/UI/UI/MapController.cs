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
        {
            visitedRoom.Add(pos);
            DrawNew(pos, roomName, doors);
        }

        cursorMask.SetTile(playerPos, null); //Remove player cursor in previous room
        playerPos = new Vector3Int(pos[0] * 4, pos[1] * 2, 0);
        cursorMask.SetTile(playerPos, tiles.Tiles[0]); //Set player cursor in new room
    }

    private void DrawNew(int[] pos, string roomName, List<Board.DoorPos> doors)
    {
        int onMapOriginX = pos[0] * 4;
        int onMapOriginY = pos[1] * 2;
        for (int i = onMapOriginX - 2; i < onMapOriginX + 3; i++)
        {
            for (int j = onMapOriginY - 1; j < onMapOriginY + 2; j++)
            {
                if (i == onMapOriginX - 1 && j == onMapOriginY)
                {
                    if (roomName == "Chest")
                        map.SetTile(new Vector3Int(i, j, 0), tiles.Tiles[3]);
                    else if (roomName == "Shop")
                        map.SetTile(new Vector3Int(i, j, 0), tiles.Tiles[4]);
                    else if (roomName == "Boss")
                        map.SetTile(new Vector3Int(i, j, 0), tiles.Tiles[5]);
                    else
                        map.SetTile(new Vector3Int(i, j, 0), tiles.Tiles[1]);
                }
                else if (j == onMapOriginY - 1 || j == onMapOriginY + 1 ||
                         i == onMapOriginX - 2 ||i == onMapOriginX + 2)
                    map.SetTile(new Vector3Int(i, j, 0), tiles.Tiles[2]);
                else
                    map.SetTile(new Vector3Int(i, j, 0), tiles.Tiles[1]);
            }
        }

        if (visitedRoom.Count != 1)
        {
            if (onMapOriginX - 2 < minPosX)
                minPosX = onMapOriginX - 2;
            if (onMapOriginX + 2 > maxPosX)
                maxPosX = onMapOriginX + 2;

            if (onMapOriginY - 1 < minPosY)
                minPosY = onMapOriginY - 1;
            if (onMapOriginY  + 1 > maxPosY)
                maxPosY = onMapOriginY + 1;
        }
        else
        {
            minPosX = onMapOriginX - 2;
            maxPosX = onMapOriginX + 2;
            minPosY = onMapOriginY - 1;
            maxPosY = onMapOriginY + 1;
        }

        print("MinX: " + minPosX + ", MaxX: " + maxPosX + ", MinY: " + minPosY + ", MaxY: " + maxPosY);
        Vector3 centerTile = map.GetCellCenterWorld(new Vector3Int((maxPosX + minPosX) / 2, (maxPosY + minPosY) / 2, 0));
        cam.transform.position = centerTile + new Vector3(0, 0, -40); //Set rhe camera position at the center of the map
        cam.orthographicSize = (maxPosX - minPosX + maxPosY - minPosY) / 2 * 0.26f;
    }
}