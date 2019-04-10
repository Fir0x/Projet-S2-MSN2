﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width = 10;
    public int height = 5;
    public int roomWidth = 17;
    public int roomHeight = 13;
    public int roomNumber = 3;
    private List<Room> roomList;
    private Transform board;

    private class RoomBase
    {
        public int[] position;
        public int roomNumber;
        public List<int[]> doorsPosition;
        public float[] anchor;

        public RoomBase(int[] arrayPosition, int roomNumber, float[] anchor)
        {
            position = arrayPosition;
            this.roomNumber = roomNumber;
            this.anchor = anchor;
            doorsPosition = new List<int[]>();
        }

        public void AddDoors(int[] doorPosition)
        {
            doorsPosition.Add(doorPosition);
        }
    }

    public void BoardCreation()
    {
        //Utility.ExecutionTime.Set(); //DEBUG
        if (roomNumber > width * height)
            throw new System.Exception("Too much rooms.");

        board = new GameObject("Board").transform;
        List<RoomBase> roomBaseList = new List<RoomBase>();
        int[] startPoint = new int[] { Random.Range(0, width - 1), Random.Range(0, height - 1) };
        RoomBase parent = new RoomBase(startPoint, 1, new float[] { startPoint[0] * roomWidth, startPoint[1] * roomHeight });
        roomBaseList.Add(parent);
        print(Utility.VisualArray<int>(startPoint));
        RoomBase actual;
        int lastX;
        int lastY;
        int newX;
        int newY;
        int nbFree;
        int k = 1;

        while (k < roomNumber)
        {
            k++;
            lastX = parent.position[0];
            lastY = parent.position[1];
            nbFree = 0;
            //print("LastX = " + lastX + "\nLastY = " + lastY); //DEBUG

            //Count how many spaces around the actual room are free
            nbFree += lastX - 1 > 0 && !roomBaseList.Exists(roomBase => roomBase.position[0] == lastX - 1 && roomBase.position[1] == lastY) ? 1 : 0;
            nbFree += lastX + 1 < width && !roomBaseList.Exists(roomBase => 
                                                                roomBase.position[0] == lastX + 1 && roomBase.position[1] == lastY) ? 1 : 0;
            nbFree += lastY - 1 > 0 && !roomBaseList.Exists(roomBase => roomBase.position[0] == lastX && roomBase.position[1] == lastY - 1) ? 1 : 0;
            nbFree += lastY + 1 < height && !roomBaseList.Exists(roombase => roombase.position[0] == lastX && roombase.position[1] == lastY + 1) ? 1 : 0;

            if (nbFree != 0)
            {

                do //Choose randomly one place in the free
                {
                    newX = Random.Range(lastX - 1, lastX + 2);
                    newY = lastY;
                    if (newX == lastX)
                        newY = Random.Range(lastY - 1, lastY + 2);

                } while (newX < 0 || newX >= width ||
                        newY < 0 || newY >= height ||
                        roomBaseList.Exists(roomBase => roomBase.position[0] == newX && roomBase.position[1] == newY));

                //print("newX = " + newPosition[0] + "\nnewY = " + newPosition[1]); //DEBUG

                actual = new RoomBase(new int[] { newX, newY }, k, new float[] { newX * roomWidth, newY * roomHeight });
                print(newX + ":" + newY);
                roomBaseList.Add(actual);
                //Add doors between the parent and his child
                if (newX > lastX)
                {
                    parent.AddDoors(new int[] { roomWidth - 1, (roomHeight - 1) / 2 });
                    actual.AddDoors(new int[] { 0, (roomHeight - 1) / 2 });
                }
                else if (newX < lastX)
                {
                    parent.AddDoors(new int[] { 0, (roomHeight - 1) / 2 });
                    actual.AddDoors(new int[] { roomWidth - 1, (roomHeight - 1) / 2 });
                }
                else if (newY > lastY)
                {
                    parent.AddDoors(new int[] { (roomWidth - 1) / 2, roomHeight - 1 });
                    actual.AddDoors(new int[] { (roomWidth - 1) / 2, 0 });
                }
                else
                {
                    parent.AddDoors(new int[] { (roomWidth - 1) / 2, 0 });
                    actual.AddDoors(new int[] { (roomWidth - 1) / 2, roomHeight - 1 });
                }

                parent = roomBaseList[Random.Range(0, roomBaseList.Count)];
            }
            else
            {
                parent = roomBaseList[Random.Range(0, roomBaseList.Count)];
                k--;
            }
        }

        Room room;
        foreach (RoomBase roomBase in roomBaseList)
        {
            room = GetComponent<Room>();
            room.RoomCreator(board, roomBase.anchor, roomBase.roomNumber, roomBase.doorsPosition);
        }

        //Utility.ExecutionTime.PrintExecutionTime(); //DEBUG
    }

    public void PrintBoard()
    {
        foreach (Room room in roomList)
            room.RoomSetup();
    }

    public void Start()
    {
        BoardCreation();
    }
}
