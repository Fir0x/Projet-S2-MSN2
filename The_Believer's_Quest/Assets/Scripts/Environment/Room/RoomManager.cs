using System.Linq;
using System.Collections.Generic;
using UnityEngine;

//Nicolas I
public class RoomManager : MonoBehaviour
{
    private List<GameObject> allEnemiesList;
    
    private int floor;
    private bool firstEntry = true;
    private List<GameObject> enemies = new List<GameObject>();
    private List<Board.DoorPos> doors;
    private TileAsset doorTiles;
    private int totalWeight = 7; //uniquement pour étage 1

    public void Init(List<Board.DoorPos> doors, int floor, TileAsset doorTiles, List<GameObject> allEnemiesList)
    {
        this.floor = floor;
        this.doors = doors;
        this.doorTiles = doorTiles;
        this.allEnemiesList = allEnemiesList;
        CreateEnemies();
    }

   private void CreateEnemies()
   {
        while (totalWeight > 0)
        {
            GameObject enemy = allEnemiesList[Random.Range(0, allEnemiesList.Count)];

            enemies.Add(enemy);

            if (totalWeight - enemy.GetComponent<Enemy>().GetWeight() < 0)
            {
                totalWeight = 0;
            }
            else
            {
                enemies.Add(enemy);
                totalWeight -= enemy.GetComponent<Enemy>().GetWeight();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        print("coucou");
        if (firstEntry && col.name == "Player")
        {
            firstEntry = false;
            
            foreach (GameObject enemy in enemies)
            {
                //Instantiate(enemy, this.transform, true) as GameObject;
            }
        }

    }

    private void DestroyEnemy(Enemy enemy)
    {
        
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
        if (doors.Contains(Board.DoorPos.Up))
            gameObject.GetComponentInChildren<LayerFront>().ClearTile();

        foreach (LayerBehind script in gameObject.GetComponentsInChildren<LayerBehind>())
        {
            script.ClearTiles(doors);
        }
    }
}
