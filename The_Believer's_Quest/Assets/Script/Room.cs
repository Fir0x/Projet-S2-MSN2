using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Room : MonoBehaviour
{
    private int width;
    private int height;
    private int[] anchor;
    private int roomNumber;
    private List<int[]> doorsPosition;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    private GameObject[,] roomArray;
    private Transform board;

    public Room(int width, int height, int[] anchor, int roomNumber, List<int[]> doorsPosition)
    {
        this.width = width;
        this.height = height;
        this.anchor = anchor;
        this.roomNumber = roomNumber;
        this.doorsPosition = doorsPosition;
        roomArray = new GameObject[height, width];
        board = new GameObject("Room " + roomNumber).transform;
        GameObject tile;
        GameObject instance;
        int[] position = new int[2];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                position[0] = j;
                position[1] = i;
                tile = (i == 0 || i == height - 1 || j == 0 || j == width - 1 || !doorsPosition.Contains(position)) ? wallTiles[0] : floorTiles[0];
                instance = Instantiate(tile, new Vector2(anchor[0] + (float)j / 3.125f, anchor[1] + (float)i / 3.125f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(board);
                roomArray[i, j] = instance;
            }
        }
    }

    public void RoomSetup()
    {
        foreach (GameObject instance in roomArray)
        {
            Instantiate(instance);
        }
    }

    public GameObject[,] GetRoom()
    {
        return roomArray;
    }
}