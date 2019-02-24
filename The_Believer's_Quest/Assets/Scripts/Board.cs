using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Range(0, 50)] public int width = 10;
    [Range(0, 50)] public int height = 5;
    [Range(2, 50)] public int roomWidth = 9;
    [Range(1, 50)] public int roomHeight = 6;
    [Range(2, 50)] public int roomNumber = 3;
    private bool[,] roomsPosition;
    private Room[,] roomsArray;
    private System.Random randomizer = new System.Random();

    public Board()
    {
        roomsPosition = new bool[height, width];
        roomsArray = new Room[height, width];
        List<int[]> usedPosition = new List<int[]>();
        print(width);
        print(height - 1);
        int[] lastPosition = new int[] { randomizer.Next(0, width - 1), randomizer.Next(0, height - 1) } ;
        int[] newPosition = new int[2];
        int lastX;
        int lastY;

        for (int i = 1; i < roomNumber; i++)
        {
            lastX = lastPosition[0];
            lastY = lastPosition[1];
            roomsPosition[lastY, lastX] = true;

            do
            {
                newPosition[0] = randomizer.Next(lastX - 1, lastX + 1);
                newPosition[1] = randomizer.Next(lastY - 1, lastY + 1);
            } while (newPosition[0] == lastX || newPosition[0] < 0 || newPosition[0] >= width || 
                    newPosition[1] == lastY || newPosition[1] < 0 || newPosition[1] >= height ||
                    usedPosition.Contains(newPosition));

            usedPosition.Add(newPosition);
        }

        List<int[]> doorsPosition = new List<int[]>();
        int roomN = 1;
        int[] anchor = new int[2];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; i++)
            {
                if (roomsPosition[j, i])
                {
                    anchor[0] = j * width;
                    anchor[1] = i * height;

                    if (j - 1 >= 0 && roomsPosition[j - 1, i])
                        doorsPosition.Add(new int[] { j - 1, i });

                    if (j + 1 < width && roomsPosition[j + 1, i])
                        doorsPosition.Add(new int[] { j + 1, i });

                    if (i - 1 >= 0 && roomsPosition[j, i - 1])
                        doorsPosition.Add(new int[] { j, i - 1 });

                    if (i + 1 < height && roomsPosition[j, i + 1])
                        doorsPosition.Add(new int[] { j, i + 1 });

                    roomsArray[j, i] = new Room(roomWidth, roomHeight, anchor, roomN, doorsPosition);
                }
            }
        }
    }

    public void PrintBoard()
    {
        foreach (Room room in roomsArray)
            room.RoomSetup();
    }

    public void Start()
    {
        new Board();
    }
}
