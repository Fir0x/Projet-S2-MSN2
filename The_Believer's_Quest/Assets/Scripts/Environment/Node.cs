using UnityEngine;
using System.Collections.Generic;

public class Node
{
    public bool walkable;
    public Vector3 worldPos;

    public int gCost;
    public int hCost;

    public int gridX;
    public int gridY;

    public Node parent;
    
    public Node(bool walkable, Vector3 pos, int gridX, int gridY)
    {
        this.walkable = walkable;
        worldPos = pos;
        this.gridX = gridX;
        this.gridY = gridY;
    }
    
    public int fCost
    {
        get => gCost + hCost;
    }
}