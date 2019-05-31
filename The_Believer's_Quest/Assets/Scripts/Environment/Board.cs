using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicolas I
public class Board : MonoBehaviour
{
    [SerializeField] private PlayerAsset playerAsset;
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 5;
    private int roomWidth = 17;
    private int roomHeight = 14;
    [SerializeField] private int roomNumber = 3;
    public static List<GameObject> roomList;
    private Transform board;

    [SerializeField] private MapController map;

    public int Width { get => width; set => width = value; }
    public int Height { get => height; set => height = value; }
    public int RoomNumber { get => roomNumber; set => roomNumber = value; }
    public MapController Map { get => map; set => map = value; }

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
        public Type type;
        public int bossDoor;                //if -1, no access to boss room. 0 is left, 1 is up, 2 is right, 3 is down

        public RoomBase(int[] arrayPosition, int roomNumber, Type type)
        {
            position = arrayPosition;
            this.roomNumber = roomNumber;
            doorsPosition = new List<DoorPos>();
            this.type = type;
            bossDoor = -1;
        }

        public void AddDoors(DoorPos doorPosition, bool isBossDoor)
        {
            if (isBossDoor)
            {
                if (doorPosition == DoorPos.Left)
                    bossDoor = 0;
                else if (doorPosition == DoorPos.Up)
                    bossDoor = 1;
                else if (doorPosition == DoorPos.Right)
                    bossDoor = 2;
                else
                    bossDoor = 3;
            }

            doorsPosition.Add(doorPosition);
        }

        public int GetBossDoor()
        {
            return bossDoor;
        }
    }


    public void BoardCreation()
    {
        if (playerAsset.Floor == 0)
        {
            board = new GameObject("Board").transform;

            GameObject room = GetComponent<Room>().HubCreator();
            GameObject hub = Instantiate(room);
            hub.transform.parent = board;
        }
        else
        {
            //Utility.ExecutionTime.Set(); //DEBUG
            if (RoomNumber > Width * Height)
                throw new System.Exception("Too much rooms.");

            board = new GameObject("Board").transform;
            List<RoomBase> roomBaseList = new List<RoomBase>();
            int[] startPoint = new int[] { Random.Range(0, Width - 1), Random.Range(0, Height - 1) };
            RoomBase parent = new RoomBase(startPoint, 1, Type.Normal);
            roomBaseList.Add(parent);
            //print(Utility.VisualArray<int>(startPoint));

            RoomBase actual;
            int lastX;
            int lastY;
            int newX;
            int newY;
            int nbFree;
            int k = 1;

            bool isBoss = false;

            int[,] boardMap = new int[height, width];
            boardMap[startPoint[1], startPoint[0]] = 1;

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
                    if (k == (int)(0.3333f * RoomNumber) || k == (int)(0.6666f * RoomNumber))
                    {
                        actual = new RoomBase(new int[] { newX, newY }, k, Type.Chest);
                    }
                    else if (k == (int)(0.5f * RoomNumber))
                        actual = new RoomBase(new int[] { newX, newY }, k, Type.Shop);
                    else if (k == RoomNumber)
                    {
                        actual = new RoomBase(new int[] { newX, newY }, k, Type.Boss);
                        isBoss = true;
                    }
                    else
                        actual = new RoomBase(new int[] { newX, newY }, k, Type.Normal);
                    //print(newX + ":" + newY); //DEBUG
                    roomBaseList.Add(actual);
                    //Add doors between the parent and his child

                    if(newX > lastX)
                    {
                        parent.AddDoors(DoorPos.Right, isBoss);
                        actual.AddDoors(DoorPos.Left, isBoss);
                    }
                    else if (newX < lastX)
                    {
                        parent.AddDoors(DoorPos.Left, isBoss);
                        actual.AddDoors(DoorPos.Right, isBoss);
                    }
                    else if (newY > lastY)
                    {
                        parent.AddDoors(DoorPos.Up, isBoss);
                        actual.AddDoors(DoorPos.Down, isBoss);
                    }
                    else
                    {
                        parent.AddDoors(DoorPos.Down, isBoss);
                        actual.AddDoors(DoorPos.Up, isBoss);
                    }

                    

                    parent = roomBaseList[Random.Range(0, roomBaseList.Count)];

                    boardMap[newY, newX] = k; //Add id of room for mapping
                    
                }
                else
                {
                    parent = roomBaseList[Random.Range(0, roomBaseList.Count)];
                    k--;
                }
            }

            Room room = GetComponent<Room>();
            foreach (RoomBase roomBase in roomBaseList)
            {
                room.RoomCreator(board, roomBase.position, new float[] { roomBase.position[0] * roomWidth + 8,
                roomBase.position[1] * roomHeight + 6 }, roomBase.roomNumber, roomBase.doorsPosition, roomBase.type, roomBase.GetBossDoor());
            }

            map.InitMap(boardMap, width, height);

            //Utility.ExecutionTime.PrintExecutionTime(); //DEBUG
        }
    }

    public void Start()
    {
        playerAsset.Floor = 0;
        BoardCreation();
    }

    public void Init()
    {
        playerAsset.Floor += 1;
        BoardCreation();
    }


}
