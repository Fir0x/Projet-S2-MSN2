using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;
//Nicolas I
[Serializable]
public class Room : MonoBehaviour
{
    public static List<GameObject> allRooms;
    
    private float[] anchor;
    private int roomNumber;
    private List<Board.DoorPos> doorsPosition;
    private GameObject room;
    [SerializeField] private PatternAsset patterns;
    [SerializeField] private GameObject player;

    public PatternAsset Patterns { get => patterns; set => patterns = value; }
    public GameObject Player { get => player; set => player = value; }

    void Awake()
    {
        allRooms = new List<GameObject>();
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
        GameObject roomPattern = Instantiate(room, new Vector3(anchor[0], anchor[1], 0), Quaternion.identity) as GameObject;
        roomPattern.transform.parent = parent;
        if (type == Board.Type.Normal)
            roomPattern.name = "Room " + roomNumber;
        else if (type == Board.Type.Chest)
            roomPattern.name = "Chest";
        else if (type == Board.Type.Shop)
            roomPattern.name = "Shop";
        else
            roomPattern.name = "Boss";
        
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

    public void RoomSetup()
    {
        
    }
}
