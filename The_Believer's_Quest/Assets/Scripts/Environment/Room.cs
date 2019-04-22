using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Room : MonoBehaviour
{
    private Grid grid;
    
    private float[] anchor;
    private int roomNumber;
    private List<int[]> doorsPosition;
    private Transform room;
    [SerializeField] public PatternAsset patterns;
    [SerializeField] protected GameObject player;
    [SerializeField] protected SpriteAsset spriteAsset;
    [SerializeField] protected GameObject wall;
    [SerializeField] protected GameObject floor;

    public void RoomCreator(Transform parent, float[] anchor, int roomNumber, List<int[]> doorsPosition)
    {
        this.anchor = anchor;
        this.roomNumber = roomNumber;
        this.doorsPosition = doorsPosition;
        
        room = patterns.Pattern[UnityEngine.Random.Range(0, patterns.Pattern.Length)].transform;
        Instantiate(room, new Vector3(anchor[0], anchor[1], 0), new Quaternion());

        if (roomNumber == 1)
        {
            player.transform.Translate(new Vector3(anchor[0] + 9 - player.transform.position.x, 
                                                   anchor[1] + 6 - player.transform.position.y,
                                                   player.transform.position.z));
        }

        
    }

    public void RoomSetup()
    {
        
    }
}
