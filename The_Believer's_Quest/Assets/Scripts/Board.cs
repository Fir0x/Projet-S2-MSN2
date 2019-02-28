using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width = 10;
    public int height = 5;
    public int roomWidth = 9;
    public int roomHeight = 6;
    public int roomNumber = 3;
    private bool[,] roomsPosition;
    private Room[,] roomsArray;
    private Room room;
    private Transform board;

    public void BoardCreation()
    {
        board = new GameObject("Board").transform;
        roomsPosition = new bool[height, width];
        roomsArray = new Room[height, width];
        List<int[]> usedPosition = new List<int[]>();
        //int[] startPoint = new int[] { Random.Range(0, width - 1), Random.Range(0, height - 1) };
        int[] startPoint = new int[] { 0, 0 }; //BEBUG
        roomsPosition[startPoint[1], startPoint[0]] = true;
        usedPosition.Add(startPoint);
        int lastX;
        int lastY;
        int nbFree;
        int k = 1;

        while (k < roomNumber)
        {
            int[] newPosition = new int[2];
            startPoint = usedPosition[Random.Range(0, usedPosition.Count)];
            lastX = startPoint[0];
            lastY = startPoint[1];
            nbFree = 0;
            //print("LastX = " + lastX + "\nLastY = " + lastY); //DEBUG

            nbFree += !usedPosition.Exists(pos => pos[0] == lastX - 1 && pos[1] == lastY) ? 1 : 0;
            nbFree += !usedPosition.Exists(pos => pos[0] == lastX + 1 && pos[1] == lastY) ? 1 : 0;
            nbFree += !usedPosition.Exists(pos => pos[0] == lastX && pos[1] == lastY - 1) ? 1 : 0;
            nbFree += !usedPosition.Exists(pos => pos[0] == lastX && pos[1] == lastY + 1) ? 1 : 0;

            if (nbFree >= 1)
            {

                do
                {
                    newPosition[0] = Random.Range(lastX - 1, lastX + 2);
                    if (newPosition[0] == lastX)
                        newPosition[1] = Random.Range(lastY - 1, lastY + 2);

                } while (newPosition[0] < 0 || newPosition[0] >= width ||
                        newPosition[1] < 0 || newPosition[1] >= height ||
                        usedPosition.Exists(pos => pos[0] == newPosition[0] && pos[1] == newPosition[1]));

                //print("newX = " + newPosition[0] + "\nnewY = " + newPosition[1]); //DEBUG
                roomsPosition[newPosition[1], newPosition[0]] = true;
                usedPosition.Add(newPosition);
                k++;
            }
            else
            {
                k--;
            }
        }
        print(usedPosition.Count); //DEBUG

        List<int[]> doorsPosition = new List<int[]>();
        int roomN = 1;
        float[] anchor = new float[2];

        /*string visual = "";
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                visual += roomsPosition[i, j];
            }
            visual += '\n';
        }
        print(visual); //DEBUG*/

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (roomsPosition[i,j])
                {
                    anchor[0] = j * roomWidth * 0.32f;
                    anchor[1] = i * roomHeight * 0.32f;
                    if (j - 1 >= 0 && roomsPosition[i, j - 1]) //left door
                        doorsPosition.Add(new int[] { 0, (roomHeight - 1) / 2 });

                    if (j + 1 < width && roomsPosition[i, j + 1]) //right door
                        doorsPosition.Add(new int[] { roomWidth - 1, (roomHeight - 1) / 2 });

                    if (i - 1 >= 0 && roomsPosition[i - 1, j]) //upper door
                        doorsPosition.Add(new int[] { (roomWidth - 1) / 2, 0 });

                    if (i + 1 < height && roomsPosition[i + 1, j]) //lower door
                        doorsPosition.Add(new int[] { (roomWidth - 1) / 2, roomHeight - 1 });

                    room = GetComponent<Room>();
                    room.RoomCreator(roomWidth, roomHeight, anchor, roomN, doorsPosition);
                    room.transform.SetParent(board);
                    roomsArray[i, j] = room;
                    doorsPosition.Clear();
                    roomN += 1;
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
        BoardCreation();
    }
}
