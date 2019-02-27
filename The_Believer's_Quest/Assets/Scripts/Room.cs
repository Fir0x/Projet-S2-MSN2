using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Room : MonoBehaviour
{
    private int width;
    private int height;
    private float[] anchor;
    private int roomNumber;
    private List<int[]> doorsPosition;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    private GameObject[,] roomArray;
    private Transform room;

    public void RoomCreator(int width, int height, float[] anchor, int roomNumber, List<int[]> doorsPosition)
    {
        this.width = width;
        this.height = height;
        this.anchor = anchor;
        this.roomNumber = roomNumber;
        this.doorsPosition = doorsPosition;

        roomArray = new GameObject[height, width];
        room = new GameObject("Room " + roomNumber).transform;
        GameObject tile;
        GameObject instance;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                tile = ((i == 0 || i == height - 1 || j == 0 || j == width - 1) &&
                    !doorsPosition.Exists(pos => pos[0] == j && pos[1] == i)) ? wallTiles[0] : floorTiles[0];

                instance = Instantiate(tile, new Vector2(anchor[0] + (float)j / 3.125f, anchor[1] + (float)i / 3.125f), 
                    Quaternion.identity) as GameObject;

                instance.transform.SetParent(room);
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
