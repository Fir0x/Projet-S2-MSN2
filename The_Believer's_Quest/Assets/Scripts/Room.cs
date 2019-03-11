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
    public GameObject player;

    public void RoomCreator(Transform parent, int width, int height, float[] anchor, int roomNumber, List<int[]> doorsPosition)
    {
        this.width = width;
        this.height = height;
        this.anchor = anchor;
        this.roomNumber = roomNumber;
        this.doorsPosition = doorsPosition;

        roomArray = new GameObject[height, width];
        room = new GameObject("Room " + roomNumber).transform;
        room.SetParent(parent);
        GameObject tile;
        GameObject instance;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (i == height - 1)
                {
                    if (j == 0)
                        tile = wallTiles[5];
                    else if (j == width - 1)
                        tile = wallTiles[6];
                    else if (!doorsPosition.Exists(pos => pos[0] == j && pos[1] == i))
                        tile = wallTiles[1];
                    else
                        tile = floorTiles[0];
                }
                else if (i == height - 2 && j > 0 && j < width - 1 && !doorsPosition.Exists(pos => pos[0] == j && pos[1] == i + 1))
                    tile = wallTiles[0];
                else if (i == 0)
                {
                    if (j == 0)
                        tile = wallTiles[8];
                    else if (j == width - 1)
                        tile = wallTiles[7];
                    else if (!doorsPosition.Exists(pos => pos[0] == j && pos[1] == i))
                        tile = wallTiles[3];
                    else
                        tile = floorTiles[0];
                }
                else if (j == 0 && !doorsPosition.Exists(pos => pos[0] == j && pos[1] == i))
                    tile = wallTiles[4];
                else if (j == width - 1 && !doorsPosition.Exists(pos => pos[0] == j && pos[1] == i))
                    tile = wallTiles[2];
                else
                    tile = floorTiles[0];

                instance = Instantiate(tile, new Vector2(anchor[0] + (float)j / 3.125f, anchor[1] + (float)i / 3.125f), 
                    Quaternion.identity) as GameObject;

                instance.transform.SetParent(room);
                roomArray[i, j] = instance;
            }
        }

        if (roomNumber == 1)
            player.transform.position = new Vector3(anchor[0] + width * 0.32f / 2, anchor[1] + height * 0.32f / 2, -0.1f);
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
