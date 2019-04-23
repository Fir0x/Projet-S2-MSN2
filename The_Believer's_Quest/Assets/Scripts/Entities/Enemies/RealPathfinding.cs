using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Entities;
using UnityEngine;

public class RealPathfinding : MonoBehaviour
{
    public GameObject enemy;
    private List<Node> finalPath;
    Grid grid;

    private List<GameObject> roomList;

    private Player player;
    private Vector2 startPosition;
    private Vector2 finishPosition;

    private float posSalleX;
    private float posSalleY;

    private void Start()
    {
        roomList = Board.roomList;
        posSalleX = (roomList[0].transform.position.x + 0.5f - player.GetFirstPos().x) / 16f;
        posSalleY = (roomList[0].transform.position.y + 0.5f - player.GetFirstPos().y) / 13f;
        
        //if(/*posX centre salle*/ < player.GetFirstPos().x)
            //posSalleX = -posSalleX;
                                                                                                                                            //il faut la position de l'ennemi !!!
       // if(/*posY centre salle*/ < player.GetFirstPos().y)
            //posSalleY = -posSalleY;

        //grid = gameObject.GetComponent<Enemies>().parent;

    }

    private void FixedUpdate()
    {
        finishPosition = player.transform.position;//new Vector2(player.GetPos().x/* - posSalleX * 15*/, player.GetPos().y/* - posSalleY * 15*/);
        startPosition = enemy.transform.position;
        FindPath(startPosition, finishPosition);
    }

    private void FindPath(Vector3 startpos, Vector3 finishpos) //pos par rapport à la salle
    {
        Node startNode = grid.GetPos(startpos);
        Node finishNode = grid.GetPos(finishpos);

        List<Node> possibleNodes = new List<Node>();
        HashSet<Node> doneNodes = new HashSet<Node>();

        possibleNodes.Add(startNode);

        while (possibleNodes.Count > 0)
        {
            Node currentNode = possibleNodes[0];
            for (int i = 1; i < possibleNodes.Count; i++)
            {
                if (possibleNodes[i].fCost() < currentNode.fCost() || (possibleNodes[i].fCost() == currentNode.fCost() && possibleNodes[i].fCost() < currentNode.fCost()))
                {
                    currentNode = possibleNodes[i];
                }
            }
            
            possibleNodes.Remove(currentNode);
            doneNodes.Add(currentNode);

            if (currentNode == finishNode)
            {
                GetPath(startNode, finishNode);
            }

            foreach (Node neighbor in currentNode.neighbors)
            {
                if (neighbor.walkable && !doneNodes.Contains(neighbor))
                {
                    int moveCost = currentNode.gCost + GetDistance(currentNode, neighbor);

                    if (moveCost < neighbor.fCost() || !possibleNodes.Contains(neighbor))
                    {
                        neighbor.gCost = moveCost;
                        neighbor.hCost = GetDistance(neighbor, finishNode);
                        neighbor.parent = currentNode;
                    }

                    if (!possibleNodes.Contains(neighbor))
                    {
                        possibleNodes.Add(neighbor);
                    }
                }
            }
        }
    }

    private void GetPath(Node startNode, Node finishNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = finishNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        
        path.Reverse();
        
        enemy.transform.Translate(new Vector3(path[0].gridX, path[0].gridY, 0));
    }

    private int GetDistance(Node node1, Node node2)
    {
        return (Math.Abs(node1.gridX - node2.gridX) + Math.Abs(node1.gridY - node2.gridY));
    }
}