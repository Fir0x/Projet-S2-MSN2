﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGrid : MonoBehaviour
{
    public Tilemap notObstacle;
    public GameObject nodePrefab;

    public int scanStartX = -7;
    public int scanStartY = 7;
    public int scanFinishX = -5;
    public int scanFinishY = 5;

    public int gridBoundX = 15;
    public int gridBoundY = 11;

    public List<GameObject> unsortedNodes;
    public GameObject[,] nodes;

    private void Awake()
    {
        unsortedNodes = new List<GameObject>();
        CreateNodes();
    }
     
    public void CreateNodes()
    {
        TileBase tile;             //dans les patterns, un tilemap ou tile null si mur 
        bool isObstacle;;

        for (int x = scanStartX; x <= scanFinishX; x++)
        {
            isObstacle = false;
            for (int y = scanStartY; y <= scanFinishY; y++)
            {
                tile = notObstacle.GetTile(new Vector3Int((int)x, (int)y, 0));
                if (tile == null)
                    isObstacle = true;

                if (!isObstacle)
                {
                    GameObject go = (GameObject)Instantiate(nodePrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                    Node node = go.GetComponent<Node>();
                    node.gridX = x + scanFinishX;
                    node.gridY = y + scanFinishY;

                    unsortedNodes.Add(go);
                }

                else
                {
                    GameObject go = (GameObject)Instantiate(nodePrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                    Node node = go.GetComponent<Node>();
                    node.gridX = x + scanFinishX;
                    node.gridY = y + scanFinishY;
                    node.walkable = false;

                    unsortedNodes.Add(go);
                }
            }
        }

        nodes = new GameObject[gridBoundX + 1, gridBoundY + 1];
        foreach (GameObject go in unsortedNodes)
        {
            Node node = go.GetComponent<Node>();
            nodes[node.gridX, node.gridY] = go;
        }

        for (int x = 0; x < gridBoundX - 1; x++)
        {
            for (int y = 0; y < gridBoundY - 1; y++)
            {
                if (nodes[x, y] != null)
                {
                    Node node = nodes[x, y].GetComponent<Node>();
                    node.neighbors = getNeighbors(x, y, gridBoundX, gridBoundY);
                }
            }
        }
    }

    public List<Node> getNeighbors(int x, int y, int width, int height)
    {
        List<Node> neighbors = new List<Node>();

        if( x > 0 && x < width - 1)
        {
            Node node = nodes[x + 1, y].GetComponent<Node>();
            neighbors.Add(node);

            Node node2 = nodes[x - 1, y].GetComponent<Node>();
            neighbors.Add(node2);

            if (y > 0 && y < height - 1)
            {
                Node node3 = nodes[x, y + 1].GetComponent<Node>();
                neighbors.Add(node3);

                Node node4 = nodes[x, y - 1].GetComponent<Node>();
                neighbors.Add(node4);
            }
            else if (y == 0)
            {
                Node node3 = nodes[x, y + 1].GetComponent<Node>();
                neighbors.Add(node3);
            }
            else
            {
                Node node4 = nodes[x, y - 1].GetComponent<Node>();
                neighbors.Add(node4);
            }
        }
        else if(x == 0)
        {
            Node node = nodes[x + 1, y].GetComponent<Node>();
            neighbors.Add(node);

            if(y > 0 && y < height - 1)
            {
                Node node3 = nodes[x, y + 1].GetComponent<Node>();
                neighbors.Add(node3);

                Node node4 = nodes[x, y - 1].GetComponent<Node>();
                neighbors.Add(node4);
            }
            else if (y == 0)
            {
                Node node3 = nodes[x, y + 1].GetComponent<Node>();
                neighbors.Add(node3);
            }
            else
            {
                Node node4 = nodes[x, y - 1].GetComponent<Node>();
                neighbors.Add(node4);
            }
        }
        else
        {
            Node node2 = nodes[x - 1, y].GetComponent<Node>();
            neighbors.Add(node2);

            if (y > 0 && y < height - 1)
            {
                Node node3 = nodes[x, y + 1].GetComponent<Node>();
                neighbors.Add(node3);

                Node node4 = nodes[x, y - 1].GetComponent<Node>();
                neighbors.Add(node4);
            }
            else if (y == 0)
            {
                Node node3 = nodes[x, y + 1].GetComponent<Node>();
                neighbors.Add(node3);
            }
            else
            {
                Node node4 = nodes[x, y - 1].GetComponent<Node>();
                neighbors.Add(node4);
            }
        }

        return neighbors;
    }

    public Node GetPos(Vector3 pos)
    {
        int x = (int) pos.x;
        int y = (int) pos.y;

        return nodes[x, y].GetComponent<Node>();
    }

    public List<Node> GetNeighboringNodes(Node node)
    {
        List<Node> neighboringNodes = new List<Node>();
        return neighboringNodes;
    }
}