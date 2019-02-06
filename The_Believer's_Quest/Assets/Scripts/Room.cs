using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Room : MonoBehaviour
{
    private GameObject[,] tiles;

    public GameObject[,] GenerateRoom(int width, int height)
    {
        tiles = new GameObject[height, width];

    }
}
