using System.Linq;
using System.Collections.Generic;
using UnityEngine;

//Nicolas I
public class RoomManager : MonoBehaviour
{
    private int floor;
    private bool firstEntry = true;
    private List<Ennemy> ennemies = new List<Ennemy>();
    private List<Board.DoorPos> doors;
    private TileAsset doorTiles;

    public void Init(List<Board.DoorPos> doors, int floor, TileAsset doorTiles)
    {
        this.floor = floor;
        this.doors = doors;
        this.doorTiles = doorTiles;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (firstEntry && col.name == "Player")
        {
            firstEntry = false;
        }
    }

    public void CloseDoors()
    {
        if (doors.Contains(Board.DoorPos.Up))
            gameObject.GetComponentInChildren<LayerFront>().SetTile(doorTiles.Tiles[(floor - 1) * 5]);

        foreach (LayerBehind script in gameObject.GetComponentsInChildren<LayerBehind>())
        {
            script.SetTiles(doors, doorTiles, floor);
        }
    }

    public void OpenDoors()
    {
        print("Open");
        if (doors.Contains(Board.DoorPos.Up))
            gameObject.GetComponentInChildren<LayerFront>().ClearTile();

        foreach (LayerBehind script in gameObject.GetComponentsInChildren<LayerBehind>())
        {
            script.ClearTiles(doors);
        }
    }
}
