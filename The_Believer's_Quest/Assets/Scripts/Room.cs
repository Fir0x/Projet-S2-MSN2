using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Room : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    private GameObject[,] roomArray;
    private Transform board;

    private void RoomSetup(int roomNumber)
    {
        roomArray = new GameObject[height, width];
        board = new GameObject("Room " + roomNumber).transform;
        GameObject tile;
        GameObject instance;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                tile = (i == 0 || i == height - 1 || j == 0 || j == width - 1) ? wallTiles[0] : floorTiles[0];
                roomArray[i, j] = tile;
                instance = Instantiate(tile, new Vector2((float) j / 3.125f, (float) i / 3.125f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(board);
            }
        }
    }
    
    private void Collision()

    public GameObject[,] GetRoom()
    {
        return roomArray;
    }

    private void Start()
    {
        RoomSetup(1);
    }
}