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

    private Room roomCreator;

    public void Init(int[] mapPos, List<Board.DoorPos> doors, int floor, TileAsset doorTiles, List<GameObject> enemiesList, Room roomCreator)
    {
        this.mapPos = mapPos;
        this.floor = floor;
        this.doors = doors;
        this.doorTiles = doorTiles;
        this.roomCreator = roomCreator;
        allEnemiesList = enemiesList;
        grid = gameObject.GetComponent<TileGrid>();
        roomPosition = this.transform.position;
        
        if (enemiesList.Count > 0)
        {
            CreateEnemies();
        }

        roomCreator.Open();
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
                    print("test");
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
                roomCreator.Close();
            }
        }

    }

    public void DestroyEnemy(GameObject enemy) //Nicolas L
    {
        enemiesRemaining -= 1;
        print(enemiesRemaining + "");
        if (enemiesRemaining == 0)
        {
            roomCreator.Open();
        }
    }

    public void CloseDoors()
    {
        StartCoroutine(Closing());
    }

    IEnumerator Closing()
    {
        yield return new WaitForSeconds(0.05f);
        /*foreach (LayerFront script in gameObject.GetComponentsInChildren<LayerFront>())
        {
            script.SetTiles(doors, doorTiles, floor);
        }

        foreach (LayerBehind script in gameObject.GetComponentsInChildren<LayerBehind>())
        {
            script.SetTiles(doors, doorTiles, floor);
        }*/

        foreach (Board.DoorPos d in doors)                      //creation of door colliders
        {
            if(d == Board.DoorPos.Up)
            {
                GameObject colliderUp = new GameObject("ColliderUp");
                colliderUp.transform.parent = this.transform;
                colliderUp.transform.position = this.transform.position + new Vector3(0.5f, 6.5f, 0);
                colliderUp.AddComponent<BoxCollider2D>();
                colliderUp.GetComponent<BoxCollider2D>().isTrigger = true;
                colliderUp.tag = "Pattern";
            }
            else if(d == Board.DoorPos.Right)
            {
                GameObject colliderRight = new GameObject("ColliderRight");
                colliderRight.transform.parent = this.transform;
                colliderRight.transform.position = this.transform.position + new Vector3(9f, 1f, 0);
                colliderRight.AddComponent<BoxCollider2D>();
                colliderRight.GetComponent<BoxCollider2D>().isTrigger = true;
                colliderRight.tag = "Pattern";
            }

            else if (d == Board.DoorPos.Down)
            {
                GameObject colliderDown = new GameObject("ColliderDown");
                colliderDown.transform.parent = this.transform;
                colliderDown.transform.position = this.transform.position + new Vector3(0.5f, -5.75f, 0);
                colliderDown.AddComponent<BoxCollider2D>();
                colliderDown.GetComponent<BoxCollider2D>().isTrigger = true;
                colliderDown.tag = "Pattern";
            }
            else
            {
                GameObject colliderLeft = new GameObject("ColliderLeft");
                colliderLeft.transform.parent = this.transform;
                colliderLeft.transform.position = this.transform.position + new Vector3(-8f, 1f, 0);
                colliderLeft.AddComponent<BoxCollider2D>();
                colliderLeft.GetComponent<BoxCollider2D>().isTrigger = true;
                colliderLeft.tag = "Pattern";
            }
        }

        gameObject.GetComponentInChildren<LayerFront>().SetTiles(doors, doorTiles, floor);
        gameObject.GetComponentInChildren<LayerBehind>().SetTiles(doors, doorTiles, floor);
    }

    public void OpenDoors()
    {
        /*foreach (LayerFront script in gameObject.GetComponentsInChildren<LayerFront>())
        {
            script.ClearTiles(doors, doorTiles, floor);
        }

        foreach (LayerBehind script in gameObject.GetComponentsInChildren<LayerBehind>())
        {
            script.ClearTiles(doors, doorTiles, floor);
        }*/

        for(int i = 4; i < transform.childCount; i++)           //destruction of door colliders
        {
            Destroy(GetComponent<Transform>().GetChild(i).gameObject);
        }

        gameObject.GetComponentInChildren<LayerFront>().ClearTiles(doors, doorTiles, floor);
        gameObject.GetComponentInChildren<LayerBehind>().ClearTiles(doors, doorTiles, floor);
    }
}
