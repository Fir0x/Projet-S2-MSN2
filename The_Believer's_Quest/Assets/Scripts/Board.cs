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
        if (roomNumber > width * height)
            throw new System.Exception("Too much rooms.");

        board = new GameObject("Board").transform;
        roomsPosition = new bool[height, width];
        roomsArray = new Room[height, width];
        List<int[]> usedPosition = new List<int[]>();
        int[] startPoint = new int[] { Random.Range(0, width - 1), Random.Range(0, height - 1) };
        //int[] startPoint = new int[] { 0, 0 }; //BEBUG
        roomsPosition[startPoint[1], startPoint[0]] = true;
        //print("Origin " + Utility.VisualArray(startPoint)); DEBUG
        usedPosition.Add(startPoint);
        int lastX;
        int lastY;
        int newX;
        int newY;
        int nbFree;
        int k = 1;

        while (k < roomNumber)
        {
            k++;
            lastX = startPoint[0];
            lastY = startPoint[1];
            nbFree = 0;
            //print("LastX = " + lastX + "\nLastY = " + lastY); //DEBUG

            nbFree += lastX - 1 > 0 && !usedPosition.Exists(pos => pos[0] == lastX - 1 && pos[1] == lastY) ? 1 : 0;
            nbFree += lastX + 1 < width && !usedPosition.Exists(pos => pos[0] == lastX + 1 && pos[1] == lastY) ? 1 : 0;
            nbFree += lastY - 1 > 0 && !usedPosition.Exists(pos => pos[0] == lastX && pos[1] == lastY - 1) ? 1 : 0;
            nbFree += lastY + 1 < height && !usedPosition.Exists(pos => pos[0] == lastX && pos[1] == lastY + 1) ? 1 : 0;

            if (nbFree >= 1)
            {

                do
                {
                    newX = Random.Range(lastX - 1, lastX + 2);
                    newY = lastY;
                    if (newX == lastX)
                        newY = Random.Range(lastY - 1, lastY + 2);

                } while (newX < 0 || newX >= width ||
                        newY < 0 || newY >= height ||
                        usedPosition.Exists(pos => pos[0] == newX && pos[1] == newY));

                //print("newX = " + newPosition[0] + "\nnewY = " + newPosition[1]); //DEBUG
                roomsPosition[newY, newX] = true;
                usedPosition.Add(new int[] { newX, newY });
                startPoint = usedPosition[Random.Range(0, usedPosition.Count)];
            }
            else
            {
                startPoint = usedPosition[Random.Range(0, usedPosition.Count)];
                k--;
            }
        }

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
