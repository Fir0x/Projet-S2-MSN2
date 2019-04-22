using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private List<Node> finalPath;
    Grid grid;
    public Transform startPosition;
    public Transform finishPosition;

    private void Start()
    {
        grid = GetComponent<Grid>();
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
    }

    private int GetDistance(Node node1, Node node2)
    {
        return (Math.Abs(node1.gridX - node2.gridX) + Math.Abs(node1.gridY - node2.gridY));
    }
}