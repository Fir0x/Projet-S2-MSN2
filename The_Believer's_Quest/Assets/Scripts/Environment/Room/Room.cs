using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Nicolas I
[Serializable]
public class Room : MonoBehaviour
{
    [SerializeField] private AllEnemiesAsset allEnemies;
    [SerializeField] private AllEnemiesAsset allBoss;
    [SerializeField] private PatternAsset patterns;
    [SerializeField] private TileAsset doorTiles;
    [SerializeField] private GameObject player;
    [SerializeField] public PlayerAsset playerAsset;
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject shopper;
    [SerializeField] private GameObject nextLevel;
    private int id;
    private Board.Type type;

    public GameObject Chest { get => chest; set => chest = value; }
    public AllEnemiesAsset AllEnemies { get => allEnemies; set => allEnemies = value; }
    public PatternAsset Patterns { get => patterns; set => patterns = value; }
    public GameObject Player { get => player; set => player = value; }
    public TileAsset DoorTiles { get => doorTiles; set => doorTiles = value; }
    public GameObject Shopper { get => shopper; set => shopper = value; }

    UnityEvent closeDoors;
    UnityEvent openDoors;

    void Awake()
    {
        if (closeDoors == null)
        {
            closeDoors = new UnityEvent();
        }

        if (openDoors == null)
            openDoors = new UnityEvent();
    }

    public GameObject HubCreator()
    {
        GameObject room = patterns.Pattern[0];
        Player.transform.position = room.transform.position + new Vector3(0.5f, 3.5f, 0f);
        GameObject nextLevelOnScene = Instantiate(nextLevel, room.transform.position + new Vector3(0.5f, 7f, 0f), Quaternion.identity) as GameObject;
        nextLevelOnScene.GetComponent<SpriteRenderer>().enabled = false;
        nextLevelOnScene.transform.SetParent(GameObject.Find("Board").transform);
        type = Board.Type.Shop;
       
        return room;
    }

    public GameObject RoomCreator(Transform parent, int[] mapPos, float[] anchor, int roomNumber, List<Board.DoorPos> doorsPosition, Board.Type type, int bossDoor)
    {
        GameObject room;
        id = roomNumber;
        this.type = type;
        if (playerAsset.Floor == 1)
        {
            if (roomNumber == 1 || type != Board.Type.Normal)
            {
                room = patterns.Pattern[1];
            }
            else
            {
                room = patterns.Pattern[UnityEngine.Random.Range(1, 10)];
            }
        }
        else
        {
            if (roomNumber == 1 || type != Board.Type.Normal)
            {
                room = patterns.Pattern[11 + 7 * (playerAsset.Floor - 2)];
            }
            else
            {
                room = patterns.Pattern[UnityEngine.Random.Range(11 + 7 * (playerAsset.Floor - 2), 17 + 7 * (playerAsset.Floor - 2))];
            }
        }

        //Creation and configuration of the GameObject
        GameObject roomPattern = Instantiate(room, new Vector3(anchor[0], anchor[1], 0), Quaternion.identity) as GameObject; //Instantiation
        roomPattern.transform.parent = parent; //Parent set
        if (type == Board.Type.Normal) //Name set
            roomPattern.name = "Room " + roomNumber;
        else if (type == Board.Type.Chest)
        {
            roomPattern.name = "Chest";
        }
        else if (type == Board.Type.Shop)
        {
            roomPattern.name = "Shop";
        }
        else
        {
            roomPattern.name = "Boss";
            roomPattern.tag = "Finish";
        }

        roomPattern.AddComponent<RoomManager>(); //Component set

        RoomManager manager = roomPattern.GetComponent<RoomManager>();
        
        if (type == Board.Type.Boss)
        {
            List<GameObject> boss = new List<GameObject>();
            boss.Add(allBoss.AllEnemies[playerAsset.Floor - 1]);
            manager.Init(mapPos, doorsPosition, playerAsset.Floor, doorTiles, boss, this, bossDoor);
        }
        else if (roomNumber == 1 || type != Board.Type.Normal)
        {
            manager.Init(mapPos, doorsPosition, playerAsset.Floor, doorTiles, new List<GameObject>(), this, bossDoor);
        }
        else
        {
            manager.Init(mapPos, doorsPosition, playerAsset.Floor, doorTiles, allEnemies.AllEnemies, this, bossDoor);
        }

        closeDoors.AddListener(manager.CloseDoors); //Add this pattern's component method to close his doors
        openDoors.AddListener(manager.OpenDoors); //Add this pattern's component method to close his doors)

        // Set player position at the first room middle
        if (roomNumber == 1)
        {
            Player.transform.Translate(new Vector3(anchor[0] + 0.5f - Player.transform.position.x, 
                                                   anchor[1] + 0.5f - Player.transform.position.y,
                                                   Player.transform.position.z));
        }


        return roomPattern;
    }

    public void Open()
    {
        //print("Invoke"); //DEBUG
        openDoors.Invoke();
    }

    public void Close()
    {
        closeDoors.Invoke();
    }

    public GameObject GetNextLevel()
    {
        return nextLevel;
    }

    public Board.Type GetRoomType()
    {
        return type;
    }
}
