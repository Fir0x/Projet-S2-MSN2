using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicolas I
public class Board : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 5;
    private int roomWidth = 17;
    private int roomHeight = 14;
    [SerializeField] private int roomNumber = 3;
    public static List<GameObject> roomList;
    private Transform board;

    public int Width { get => width; set => width = value; }
    public int Height { get => height; set => height = value; }
    public int RoomNumber { get => roomNumber; set => roomNumber = value; }

    public enum DoorPos
    {
        Up,
        Right,
        Down,
        Left
    };

    public enum Type
    {
        Normal,
        Chest,
        Shop,
        Boss
    };

    private class RoomBase
    {
        public int[] position;
        public int roomNumber;
        public List<DoorPos> doorsPosition;
        public float[] anchor;
        public Type type;

        public RoomBase(int[] arrayPosition, int roomNumber, float[] anchor, Type type)
        {
            position = arrayPosition;
            this.roomNumber = roomNumber;
            this.anchor = anchor;
            doorsPosition = new List<DoorPos>();
            this.type = type;
        }

        public void AddDoors(DoorPos doorPosition)
        {
            doorsPosition.Add(doorPosition);
        }
    }

    public void BoardCreation()
    {
        //Utility.ExecutionTime.Set(); //DEBUG
        if (RoomNumber > Width * Height)
            throw new System.Exception("Too much rooms.");

        board = new GameObject("Board").transform;
        List<RoomBase> roomBaseList = new List<RoomBase>();
        int[] startPoint = new int[] { Random.Range(0, Width - 1), Random.Range(0, Height - 1) };
        RoomBase parent = new RoomBase(startPoint, 1, new float[] { startPoint[0] * roomWidth, startPoint[1] * roomHeight }, Type.Normal);
        roomBaseList.Add(parent);
        //print(Utility.VisualArray<int>(startPoint));
        RoomBase actual;
        int lastX;
        int lastY;
        int newX;
        int newY;
        int nbFree;
        int k = 1;

        while (k < RoomNumber)
        {
            k++;
            lastX = parent.position[0];
            lastY = parent.position[1];
            nbFree = 0;
            //print("LastX = " + lastX + "\nLastY = " + lastY); //DEBUG

            //Count how many spaces around the actual room are free
            nbFree += lastX - 1 > 0 && !roomBaseList.Exists(roomBase => roomBase.position[0] == lastX - 1 && roomBase.position[1] == lastY) ? 1 : 0;
            nbFree += lastX + 1 < Width && !roomBaseList.Exists(roomBase => 
                                                                roomBase.position[0] == lastX + 1 && roomBase.position[1] == lastY) ? 1 : 0;
            nbFree += lastY - 1 > 0 && !roomBaseList.Exists(roomBase => roomBase.position[0] == lastX && roomBase.position[1] == lastY - 1) ? 1 : 0;
            nbFree += lastY + 1 < Height && !roomBaseList.Exists(roombase => roombase.position[0] == lastX && roombase.position[1] == lastY + 1) ? 1 : 0;

            if (nbFree != 0)
            {

                do //Choose randomly one place in the free
                {
                    newX = Random.Range(lastX - 1, lastX + 2);
                    newY = lastY;
                    if (newX == lastX)
                        newY = Random.Range(lastY - 1, lastY + 2);

                } while (newX < 0 || newX >= Width ||
                        newY < 0 || newY >= Height ||
                        roomBaseList.Exists(roomBase => roomBase.position[0] == newX && roomBase.position[1] == newY));

                //print("newX = " + newPosition[0] + "\nnewY = " + newPosition[1]); //DEBUG
                if (k == (int)(0.3333f * RoomNumber))
                {
                    actual = new RoomBase(new int[] { newX, newY }, k, new float[] { newX * roomWidth, newY * roomHeight }, Type.Chest);
                }
                else if (k == (int)(0.5f * RoomNumber))
                    actual = new RoomBase(new int[] { newX, newY }, k, new float[] { newX * roomWidth, newY * roomHeight }, Type.Shop);
                else if (k == (int)(0.6666f * RoomNumber))
                    actual = new RoomBase(new int[] { newX, newY }, k, new float[] { newX * roomWidth, newY * roomHeight }, Type.Chest);
                else if (k == RoomNumber)
                    actual = new RoomBase(new int[] { newX, newY }, k, new float[] { newX * roomWidth, newY * roomHeight }, Type.Boss);
                else
                    actual = new RoomBase(new int[] { newX, newY }, k, new float[] { newX * roomWidth, newY * roomHeight }, Type.Normal);
                //print(newX + ":" + newY); //DEBUG
                roomBaseList.Add(actual);
                //Add doors between the parent and his child
                if (newX > lastX)
                {
                    parent.AddDoors(DoorPos.Right);
                    actual.AddDoors(DoorPos.Left);
                }
                else if (newX < lastX)
                {
                    parent.AddDoors(DoorPos.Left);
                    actual.AddDoors(DoorPos.Right);
                }
                else if (newY > lastY)
                {
                    parent.AddDoors(DoorPos.Down);
                    actual.AddDoors(DoorPos.Up);
                }
                else
                {
                    parent.AddDoors(DoorPos.Up);
                    actual.AddDoors(DoorPos.Down);
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
            room.RoomCreator(board, roomBase.anchor, roomBase.roomNumber, roomBase.doorsPosition, roomBase.type);
        }

        //Utility.ExecutionTime.PrintExecutionTime(); //DEBUG
    }

    public void Start()
    {
        BoardCreation();
    }
    
    
}
