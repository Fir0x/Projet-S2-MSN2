using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Nicolas I
public class RoomManager : MonoBehaviour
{
    private List<GameObject> allEnemiesList;

    private int[] mapPos;
    private int floor;
    private bool firstEntry = true;
    private List<GameObject> enemies = new List<GameObject>();
    private List<Board.DoorPos> doors;
    private TileAsset doorTiles;
    private int totalWeight = 7; //uniquement pour étage 1
    private int enemiesRemaining;

    private Vector3 roomPosition;
    private TileGrid grid;

    public void Init(int[] mapPos, List<Board.DoorPos> doors, int floor, TileAsset doorTiles, List<GameObject> enemiesList)
    {
        this.mapPos = mapPos;
        this.floor = floor;
        this.doors = doors;
        this.doorTiles = doorTiles;
        allEnemiesList = enemiesList;
        grid = gameObject.GetComponent<TileGrid>();
        roomPosition = this.transform.position;
        
        if (enemiesList.Count > 0)
        {
            CreateEnemies();
        }
        
        OpenDoors();
    }

   private void CreateEnemies()
   {
        while (totalWeight > 0)
        {
            GameObject enemy = allEnemiesList[Random.Range(0, allEnemiesList.Count)];

            if (totalWeight - enemy.GetComponent<Enemy>().GetWeight() >= 0)
            {
                enemies.Add(enemy);
                totalWeight -= enemy.GetComponent<Enemy>().GetWeight();
            }
        }

        enemiesRemaining = enemies.Count;
   }

    private void OnTriggerEnter2D(Collider2D col) //Nicolas L
    {
        if (firstEntry && col.CompareTag("Player"))
        {
            firstEntry = false;

            bool posOk;
            int x = 0;
            int y = 0;
            
            foreach (GameObject enemy in enemies)        //apparition ennemis
            {
                posOk = false;
                while (!posOk)
                {
                    x = Random.Range(-7, 7);
                    y = Random.Range(-5, 5);

                    if (grid.NodeFromPos(roomPosition + new Vector3(x, y, 0)).walkable)
                    {
                        posOk = true;
                    }
                }
                
                GameObject enemyOnScene = Instantiate(enemy, roomPosition + new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                enemyOnScene.transform.parent = this.transform;
            }

            if (enemiesRemaining > 0)
            {
                CloseDoors();
            }
        }

    }

    public void DestroyEnemy(GameObject enemy) //Nicolas L
    {
        enemiesRemaining -= 1;
        print(enemiesRemaining + "");
        if (enemiesRemaining == 0)
        {
            OpenDoors();
        }
    }

    public void CloseDoors()
    {
        StartCoroutine(Closing());
    }

    IEnumerator Closing()
    {
        yield return new WaitForSeconds(0.05f);
        if (doors.Contains(Board.DoorPos.Up))
            gameObject.GetComponentInChildren<LayerFront>().SetTile(doorTiles.Tiles[1 + (floor - 1) * 5]);

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
