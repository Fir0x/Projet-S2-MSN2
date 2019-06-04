using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Nicolas I && Nicolas L && Maxence
public class RoomManager : MonoBehaviour
{
    private List<GameObject> allEnemiesList;
    [SerializeField] private AllEnemiesAsset allBoss;

    private int[] mapPos;
    private int floor;
    private bool firstEntry = true;
    private List<GameObject> enemies = new List<GameObject>();
    private List<Board.DoorPos> doors;
    private TileAsset doorTiles;
    private int totalWeight; //uniquement pour étage 1
    private int enemiesRemaining;

    private bool testForBoss;
    private int bossDoor;
    private bool isBossRoom;

    private GameObject nextLevel;
    private GameObject chest;
    private GameObject gandulf;

    private Vector3 roomPosition;
    private TileGrid grid;

    private Room roomCreator;

    private Board.Type roomType;

    public void Init(int[] mapPos, List<Board.DoorPos> doors, int floor, TileAsset doorTiles, List<GameObject> enemiesList, Room roomCreator, int bossDoor)
    {
        gandulf = roomCreator.Shopper;
        roomType = roomCreator.GetRoomType();
        totalWeight = 7 * floor;
        nextLevel = roomCreator.GetNextLevel();
        chest = roomCreator.Chest;

        this.bossDoor = bossDoor;
        this.mapPos = mapPos;
        this.floor = floor;
        this.doors = doors;
        this.doorTiles = doorTiles;
        this.roomCreator = roomCreator;
        allEnemiesList = enemiesList;
        grid = gameObject.GetComponent<TileGrid>();
        roomPosition = transform.position;
        
        if (roomCreator.GetRoomType() == Board.Type.Chest)
        {
            GameObject chestOnScene = Instantiate(chest, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity) as GameObject;
            chestOnScene.transform.parent = gameObject.transform;
        }
        else if (roomCreator.GetRoomType() == Board.Type.Shop)
        {
            GameObject shopOnScene = Instantiate(gandulf, transform.position + new Vector3(-4.5f, 3f, 0), Quaternion.identity) as GameObject;
            shopOnScene.transform.parent = gameObject.transform;
        }
        else if (roomCreator.GetRoomType() == Board.Type.Boss)
        {
            isBossRoom = true;
            testForBoss = true;
            enemies.Add(enemiesList[0]);

        }
        else if (enemiesList.Count > 0)
        {
            CreateEnemies();
        }

        LayerBehind layerB = GetComponent<LayerBehind>();                               //to make the boss room be opened
        LayerFront layerF = GetComponent<LayerFront>();

        if (bossDoor == -1)
        {
            layerB.ClearTiles(doors, doorTiles, floor);
            layerF.ClearTiles(doors, doorTiles, floor);
        }
        if (bossDoor == 0)
        {
            layerB.ClearTiles(new List<Board.DoorPos> { Board.DoorPos.Left }, doorTiles, floor);
            layerF.ClearTiles(new List<Board.DoorPos> { Board.DoorPos.Left }, doorTiles, floor);
        }
        else if (bossDoor == 1)
        {
            layerB.ClearTiles(new List<Board.DoorPos> { Board.DoorPos.Up }, doorTiles, floor);
            layerF.ClearTiles(new List<Board.DoorPos> { Board.DoorPos.Up }, doorTiles, floor);
        }
        else if (bossDoor == 2)
        {
            layerB.ClearTiles(new List<Board.DoorPos> { Board.DoorPos.Right }, doorTiles, floor);
            layerF.ClearTiles(new List<Board.DoorPos> { Board.DoorPos.Right }, doorTiles, floor);
        }
        else if (bossDoor == 3)
        {
            layerB.ClearTiles(new List<Board.DoorPos> { Board.DoorPos.Down }, doorTiles, floor);
            layerF.ClearTiles(new List<Board.DoorPos> { Board.DoorPos.Down }, doorTiles, floor);
        }

        roomCreator.Open();
    }

   private void CreateEnemies()
   {
        int count = 0;
        int security = 0;
        while (totalWeight > 0)
        {
            GameObject enemy;
            if (floor == 1)
            {
                enemy = allEnemiesList[Random.Range(0, 3)];
            }
            else if (floor == 2)
            {
                enemy = allEnemiesList[Random.Range(3, 5)];
            }
            else if (floor == 3)
            {
                enemy = allEnemiesList[Random.Range(5, 6)];
            }
            else
            {
                enemy = allEnemiesList[Random.Range(0, allEnemiesList.Count)];
            }

            if (totalWeight - enemy.GetComponent<Enemy>().GetWeight() >= 0)
            {
                enemies.Add(enemy);
                totalWeight -= enemy.GetComponent<Enemy>().GetWeight();
                count++;
            }
            security++;
            if (security == 40)
                break;
        }

        enemiesRemaining = count;
   }

    public void AddEnemies(GameObject[] newEnemies)
    {
        foreach(GameObject enemy in newEnemies)
        {
            enemies.Add(enemy);
        }

        SpawnEnemies();
    }

    public void SpawnEnemiesForThirdBoss(GameObject enemy)
    {
        if (enemy == null)
        {
            OpenDoors();
            GameObject outdoor = Instantiate(nextLevel, transform.position + new Vector3(0, 3f, 0), Quaternion.identity) as GameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D col) //Nicolas L
    {
        GameObject.Find("Player").GetComponent<Player>().RoomType = roomType;
        if (testForBoss)
        {
            GameObject.Find("SoundManager").GetComponent<SoundManager>().ChangeBO(floor + 2);
        }

        if (firstEntry && col.CompareTag("Player"))
        {
            SpawnEnemies();

            enemiesRemaining = enemies.Count;
            if (enemiesRemaining > 0)
            {
                roomCreator.Close();
            }
            firstEntry = false;
        }
        MapController.mapInstance.EnterRoom(mapPos, gameObject.name, doors);
    }

    public void SpawnEnemies()
    {
        bool posOk;
        int x = 0;
        int y = 0;
        
        if (gameObject.CompareTag("Finish"))         //apparition boss
        {
            if (testForBoss)
            {
                testForBoss = false;
                GameObject bossOnScene = Instantiate(enemies[0], roomPosition, Quaternion.identity) as GameObject;
                bossOnScene.transform.parent = transform;
                //bossOnScene.GetComponent<BossLifebar>().SliderDisappear();
            }
            else
            {
                Vector3 player = GameObject.Find("Player").transform.position;
                bool noBoss = false;
                foreach (GameObject enemy in enemies)        //apparition ennemis sauf boss(pour phases de boss)
                {
                    if(noBoss)
                    {
                        posOk = false;
                        while (!posOk)
                        {
                            x = Random.Range(-7, 7);
                            y = Random.Range(-5, 5);

                            if (grid.NodeFromPos(roomPosition + new Vector3(x, y, 0)).walkable && (x <= player.x - 1|| x >= player.x + 1) && (y <= player.y - 1 || y >= player.y + 1))
                            {
                                posOk = true;
                            }
                        }

                        GameObject enemyOnScene = Instantiate(enemy, roomPosition + new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                        enemyOnScene.transform.parent = transform;
                    }
                    else
                    {
                        noBoss = true;
                    }
                }
            }
        }
        else
        {
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
                enemyOnScene.transform.parent = transform;
            }
        }

        enemiesRemaining = enemies.Count;
        
    }

    public void DestroyEnemy(GameObject enemy) //Nicolas L
    {
        Destroy(enemy);
        enemiesRemaining -= 1;
        if (enemiesRemaining == 0)
        {
            roomCreator.Open();
            if (isBossRoom)
            {
                GameObject outdoor = Instantiate(nextLevel, transform.position + new Vector3(0, 3f, 0), Quaternion.identity) as GameObject;
            }
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
        SoundManager soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        soundManager.PlaySingle(soundManager.lfx[2]);

        foreach (Board.DoorPos d in doors)                      //creation of door colliders
        {
            if(d == Board.DoorPos.Up)
            {
                GameObject colliderUp = new GameObject("ColliderUp");
                colliderUp.transform.parent = transform;
                colliderUp.transform.position = transform.position + new Vector3(0.5f, 6.5f, 0);
                colliderUp.AddComponent<BoxCollider2D>();
                colliderUp.GetComponent<BoxCollider2D>().isTrigger = true;
                colliderUp.tag = "Pattern";
                colliderUp.layer = LayerMask.NameToLayer("Obstacle");
            }
            else if(d == Board.DoorPos.Right)
            {
                GameObject colliderRight = new GameObject("ColliderRight");
                colliderRight.transform.parent = transform;
                colliderRight.transform.position = transform.position + new Vector3(9f, 1f, 0);
                colliderRight.AddComponent<BoxCollider2D>();
                colliderRight.GetComponent<BoxCollider2D>().isTrigger = true;
                colliderRight.tag = "Pattern";
                colliderRight.layer = LayerMask.NameToLayer("Obstacle");
            }

            else if (d == Board.DoorPos.Down)
            {
                GameObject colliderDown = new GameObject("ColliderDown");
                colliderDown.transform.parent = transform;
                colliderDown.transform.position = transform.position + new Vector3(0.5f, -5.79f, 0);
                colliderDown.AddComponent<BoxCollider2D>();
                colliderDown.GetComponent<BoxCollider2D>().isTrigger = true;
                colliderDown.tag = "Pattern";
                colliderDown.layer = LayerMask.NameToLayer("Obstacle");
            }
            else
            {
                GameObject colliderLeft = new GameObject("ColliderLeft");
                colliderLeft.transform.parent = transform;
                colliderLeft.transform.position = transform.position + new Vector3(-8f, 1f, 0);
                colliderLeft.AddComponent<BoxCollider2D>();
                colliderLeft.GetComponent<BoxCollider2D>().isTrigger = true;
                colliderLeft.tag = "Pattern";
                colliderLeft.layer = LayerMask.NameToLayer("Obstacle");
            }
        }
        
        gameObject.GetComponentInChildren<LayerFront>().SetTiles(doors, doorTiles, floor, testForBoss, bossDoor);
        gameObject.GetComponentInChildren<LayerBehind>().SetTiles(doors, doorTiles, floor, testForBoss, bossDoor);
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
        SoundManager soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        soundManager.PlaySingle(soundManager.lfx[2]);
        for (int i = 4; i < transform.childCount; i++)           //destruction of door colliders
        {
            if(GetComponent<Transform>().GetChild(i).gameObject.tag != "Chest" && GetComponent<Transform>().GetChild(i).gameObject.tag != "Shop")
            {
                Destroy(GetComponent<Transform>().GetChild(i).gameObject);
            }
        }

        gameObject.GetComponentInChildren<LayerFront>().ClearTiles(doors, doorTiles, floor);
        gameObject.GetComponentInChildren<LayerBehind>().ClearTiles(doors, doorTiles, floor);
    }
}
