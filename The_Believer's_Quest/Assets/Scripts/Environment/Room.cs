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
    private GameObject[,] roomArray;
    private Transform room;
    [SerializeField] protected GameObject player;
    [SerializeField] protected SpriteAsset spriteAsset;
    [SerializeField] protected GameObject wall;
    [SerializeField] protected GameObject floor;

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
        Sprite tile;
        bool is_wall = true;
        GameObject sprite;
        GameObject instance;
        
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (i == height - 1) //haut
                {
                    if (j == 0) //coin gauche
                        tile = spriteAsset.Wall[8];
                    else if (j == width - 1) //coin droit
                        tile = spriteAsset.Wall[2];
                    else if (!doorsPosition.Exists(pos => pos[0] == j && pos[1] == i)) //mur
                        tile = spriteAsset.Wall[1];
                    else
                    {
                        tile = spriteAsset.Floor[0]; //porte
                        is_wall = false;
                    }
                }
                else if (i == height - 2 && j > 0 && j < width - 1 && !doorsPosition.Exists(pos => pos[0] == j && pos[1] == i + 1)) //haut2
                    tile = spriteAsset.Wall[0];
                else if (i == 0) //bas
                {
                    if (j == 0) //coin gauche
                        tile = spriteAsset.Wall[6];
                    else if (j == width - 1) //coin droit
                        tile = spriteAsset.Wall[4];
                    else if (!doorsPosition.Exists(pos => pos[0] == j && pos[1] == i)) //mur
                        tile = spriteAsset.Wall[5];
                    else
                    {
                        tile = spriteAsset.Floor[0]; //porte
                        is_wall = false;
                    }
                }
                else if (j == 0 && !doorsPosition.Exists(pos => pos[0] == j && pos[1] == i)) //gauche
                    tile = spriteAsset.Wall[7];
                else if (j == width - 1 && !doorsPosition.Exists(pos => pos[0] == j && pos[1] == i)) //droite
                    tile = spriteAsset.Wall[3];
                else
                {
                    tile = spriteAsset.Floor[0]; //sol ou porte
                    is_wall = false;
                }

                sprite = is_wall ? wall : floor;
                instance = Instantiate(sprite, new Vector2(anchor[0] + (float)j , anchor[1] + (float)i ), 
                    Quaternion.identity) as GameObject;
                instance.GetComponent<SpriteRenderer>().sprite = tile;

                instance.transform.SetParent(room);
                roomArray[i, j] = instance;
                is_wall = true;
            }
        }

        if (roomNumber == 1)
        {
            player.transform.Translate(new Vector3(anchor[0] + width / 2 - player.transform.position.x, 
                                                   anchor[1] + height / 2 - player.transform.position.y,
                                                   player.transform.position.z));
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
