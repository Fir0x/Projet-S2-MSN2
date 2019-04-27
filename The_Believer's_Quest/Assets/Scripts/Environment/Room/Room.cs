using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.Events;

//Nicolas I
[Serializable]
public class Room : MonoBehaviour
{
    public static List<GameObject> allRooms = new List<GameObject>();
    
    private float[] anchor;
    private int roomNumber;
    private List<Board.DoorPos> doorsPosition;
    private GameObject room;

    [SerializeField] private PatternAsset patterns;
    [SerializeField] private TileAsset doorTiles;
    [SerializeField] private GameObject player;
    [SerializeField] public PlayerAsset playerAsset;

    public PatternAsset Patterns { get => patterns; set => patterns = value; }
    public GameObject Player { get => player; set => player = value; }
    public TileAsset DoorTiles { get => doorTiles; set => doorTiles = value; }

    public UnityEvent closeDoors;
    public UnityEvent openDoors;

    private void Start()
    {
        if (closeDoors == null)
            closeDoors = new UnityEvent();

        if (openDoors == null)
            openDoors = new UnityEvent();

        openDoors.AddListener(RoomSetup);
    }

    public GameObject RoomCreator(Transform parent, float[] anchor, int roomNumber, List<Board.DoorPos> doorsPosition, Board.Type type)
    {
        this.anchor = anchor;
        this.roomNumber = roomNumber;
        this.doorsPosition = doorsPosition;

        if (roomNumber == 1 || type != Board.Type.Normal)
        {
            room = Patterns.Pattern[0];
        }
        else
        {
            room = Patterns.Pattern[UnityEngine.Random.Range(0, Patterns.Pattern.Length)];
        }

        //Creation and configuration of the GameObject
        GameObject roomPattern = Instantiate(room, new Vector3(anchor[0], anchor[1], 0), Quaternion.identity) as GameObject; //Instantiation

        roomPattern.transform.parent = parent; //Parent set
        if (type == Board.Type.Normal) //Name set
            roomPattern.name = "Room " + roomNumber;
        else if (type == Board.Type.Chest)
            roomPattern.name = "Chest";
        else if (type == Board.Type.Shop)
            roomPattern.name = "Shop";
        else
            roomPattern.name = "Boss";

        roomPattern.AddComponent<RoomManager>(); //Component set

        RoomManager manager = roomPattern.GetComponent<RoomManager>();
        manager.Init(doorsPosition, playerAsset.Floor, doorTiles);

        closeDoors.AddListener(manager.CloseDoors); //Add this pattern's component method to close his doors
        openDoors.AddListener(manager.OpenDoors); //Add this pattern's component method to close his doors)

        // Set player position at the first room middle
        if (roomNumber == 1)
        {
            Player.transform.Translate(new Vector3(anchor[0] + 0.5f - Player.transform.position.x, 
                                                   anchor[1] + 0.5f - Player.transform.position.y,
                                                   Player.transform.position.z));
        }

        roomPattern.AddComponent<EnemiesRoom>();
        allRooms.Add(roomPattern);

        return roomPattern;
    }

    public void Open()
    {
        print(openDoors.GetPersistentEventCount());
        openDoors.Invoke();
    }

    public void Close()
    {
        closeDoors.Invoke();
    }

    public void RoomSetup()
    {
        print("test");
    }
}
