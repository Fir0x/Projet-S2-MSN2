﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Nicolas I
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
    private int totalWeight = 7; //uniquement pour étage 1
    private int enemiesRemaining;
    private bool testForBoss;
    private int bossDoor;

    private Vector3 roomPosition;
    private TileGrid grid;

    private Room roomCreator;

    public void Init(int[] mapPos, List<Board.DoorPos> doors, int floor, TileAsset doorTiles, List<GameObject> enemiesList, Room roomCreator, int bossDoor)
    {
        this.bossDoor = bossDoor;
        this.mapPos = mapPos;
        this.floor = floor;
        this.doors = doors;
        this.doorTiles = doorTiles;
        this.roomCreator = roomCreator;
        allEnemiesList = enemiesList;
        grid = gameObject.GetComponent<TileGrid>();
        roomPosition = this.transform.position;
        
        if(enemiesList.Count == 1)
        {
            testForBoss = true;
            enemies.Add(enemiesList[0]);

            LayerBehind layerB = GetComponent<LayerBehind>();                               //to make the boss room be opened
            LayerFront layerF = GetComponent<LayerFront>();

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


        }
        else if (enemiesList.Count > 0)
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

    public void AddEnemies(GameObject[] newEnemies)
    {
        foreach(GameObject enemy in newEnemies)
        {
            enemies.Add(enemy);
        }

        SpawnEnemies();
    }

    private void OnTriggerEnter2D(Collider2D col) //Nicolas L
    {
        if (firstEntry && col.CompareTag("Player"))
        {
            SpawnEnemies();

            enemiesRemaining = enemies.Count;
            if (enemiesRemaining > 0)
            {
                print("allo");
                roomCreator.Close();
            }
            firstEntry = false;
        }
    }

    public void SpawnEnemies()
    {
        bool posOk;
        int x = 0;
        int y = 0;
        
        if(gameObject.CompareTag("Finish") && testForBoss)         //apparition boss
        {
            testForBoss = false;
            GameObject bossOnScene = Instantiate(enemies[0], roomPosition, Quaternion.identity) as GameObject;
            bossOnScene.transform.parent = this.transform;
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
                enemyOnScene.transform.parent = this.transform;
            }
        }
        
    }

    public void DestroyEnemy(GameObject enemy) //Nicolas L
    {
        enemiesRemaining -= 1;
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
                colliderUp.layer = LayerMask.NameToLayer("Obstacle");
            }
            else if(d == Board.DoorPos.Right)
            {
                GameObject colliderRight = new GameObject("ColliderRight");
                colliderRight.transform.parent = this.transform;
                colliderRight.transform.position = this.transform.position + new Vector3(9f, 1f, 0);
                colliderRight.AddComponent<BoxCollider2D>();
                colliderRight.GetComponent<BoxCollider2D>().isTrigger = true;
                colliderRight.tag = "Pattern";
                colliderRight.layer = LayerMask.NameToLayer("Obstacle");
            }

            else if (d == Board.DoorPos.Down)
            {
                GameObject colliderDown = new GameObject("ColliderDown");
                colliderDown.transform.parent = this.transform;
                colliderDown.transform.position = this.transform.position + new Vector3(0.5f, -5.75f, 0);
                colliderDown.AddComponent<BoxCollider2D>();
                colliderDown.GetComponent<BoxCollider2D>().isTrigger = true;
                colliderDown.tag = "Pattern";
                colliderDown.layer = LayerMask.NameToLayer("Obstacle");
            }
            else
            {
                GameObject colliderLeft = new GameObject("ColliderLeft");
                colliderLeft.transform.parent = this.transform;
                colliderLeft.transform.position = this.transform.position + new Vector3(-8f, 1f, 0);
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

        for(int i = 4; i < transform.childCount; i++)           //destruction of door colliders
        {
            Destroy(GetComponent<Transform>().GetChild(i).gameObject);
        }

        gameObject.GetComponentInChildren<LayerFront>().ClearTiles(doors, doorTiles, floor);
        gameObject.GetComponentInChildren<LayerBehind>().ClearTiles(doors, doorTiles, floor);
    }
}
