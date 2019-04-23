using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;

[Serializable]
public class Room : MonoBehaviour
{
    private Grid grid;
    
    private float[] anchor;
    private int roomNumber;
    private List<int[]> doorsPosition;
    private GameObject room;
    [SerializeField] public PatternAsset patterns;
    [SerializeField] protected GameObject player;
    [SerializeField] protected SpriteAsset spriteAsset;
    [SerializeField] protected GameObject wall;
    [SerializeField] protected GameObject floor;
    
    public GameObject RoomCreator(Transform parent, float[] anchor, int roomNumber, List<int[]> doorsPosition, bool spawningRoom)
    {
        this.anchor = anchor;
        this.roomNumber = roomNumber;
        this.doorsPosition = doorsPosition;

        if (spawningRoom)
        {
            room = patterns.Pattern[0];
        }
        else
        {
            room = patterns.Pattern[UnityEngine.Random.Range(0, patterns.Pattern.Length)];
        }
        GameObject roomPattern = Instantiate(room, new Vector3(anchor[0], anchor[1], 0), Quaternion.identity) as GameObject;
        roomPattern.transform.parent = parent;
        
        if (roomNumber == 1)
        {
            player.transform.Translate(new Vector3(anchor[0] + 0.5f - player.transform.position.x, 
                                                   anchor[1] + 0.5f - player.transform.position.y,
                                                   player.transform.position.z));
        }
        roomPattern.AddComponent<EnemiesRoom>();
        

        return roomPattern;
    }

    public void RoomSetup()
    {
        
    }
}
