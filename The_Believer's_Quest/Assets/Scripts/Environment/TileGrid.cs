using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGrid : MonoBehaviour
{
    public Tilemap isObstacle;
    private Node[,] grid;

    private int gridSizeX = 15;
    private int gridSixeY = 11;

    private Vector2 startScan;
    private Vector2 finishScan;

    void Start()
    {
        Vector2 position = this.transform.position;
        startScan = new Vector2(position.x - 7, position.y - 5);
        finishScan = new Vector2(position.x + 7, position.y + 5);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSixeY];
        for (float x = startScan.x; x <= finishScan.x; x++)
        {
            for (float y = startScan.y; y <= finishScan.y; y++)
            {
                TileBase tile = isObstacle.GetTile(new Vector3Int((int) x, (int) y, 0));
                bool walkable = tile == null;
                Node node = new Node(walkable, new Vector3((int) x, (int) y, 0), (int) (x - startScan.x), (int) (y - startScan.y));

                grid[(int)x - (int)startScan.x, (int)y - (int)startScan.y] = node;
            }
        }
    }

    public Node NodeFromPos(Vector3 worldPos)
    {
        return grid[(int) worldPos.x  - (int)startScan.x, (int) worldPos.y - (int) startScan.y];

    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x != 0 || y != 0)
                {
                    int neighX = node.gridX + x;
                    int neighY = node.gridY + y;
                    if (neighX >= 0 && neighX < gridSizeX && neighY >= 0 && neighY < gridSixeY)
                    {
                        neighbors.Add(grid[neighX, neighY]);
                    }
                }
            }
        }
        
        return neighbors;
    }
}
