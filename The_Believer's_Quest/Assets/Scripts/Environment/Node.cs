using UnityEngine;
using System.Collections.Generic;

public class Node
{
    public int gCost;
    public int hCost;
    public int gridX, gridY;
    public bool walkable = true;
    public List<Node> neighbors;
    public Node parent;

    public int fCost()
    {
        return gCost + hCost;
    }
}