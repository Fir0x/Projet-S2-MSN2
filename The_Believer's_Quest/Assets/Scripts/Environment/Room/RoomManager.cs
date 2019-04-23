using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicolas I
public class RoomManager : MonoBehaviour
{
    private bool firstEntry = true;
    private List<Ennemy> ennemies = new List<Ennemy>();
    private List<Board.DoorPos> doors;
    [SerializeField] private TileAsset tiles;

    public List<Board.DoorPos> Doors { get => doors; set => doors = value; }
    public TileAsset Tiles { get => tiles; set => tiles = value; }

    private void OnTriggerEnter(Collider col)
    {
        if (firstEntry && col.name == "Player")
        {
            firstEntry = false;
        }
    }

    private void SetDoorsFront()
    {

    }

    private void SetDoorsBehind()
    {

    }

    public void ClearDoorsFront()
    {

    }

    public void ClearDoorsBehind()
    {

    }
}
