using System;
using System.Collections.Generic;
using UnityEngine;
//Nicolas L
namespace Entities
{
    public class
        RealPathfinding : MonoBehaviour
    {
        private TileGrid grid;

        void Awake()
        {
            grid = GetComponent<TileGrid>();
        }

        public Node FindPath(Vector3 startPos, Vector3 finishPos)
        {
            Node startNode = grid.NodeFromPos(startPos);
            Node finishNode = grid.NodeFromPos(finishPos);
            Node nextNode = null;

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            
            openSet.Add(startNode);
            bool temp = true;
            while (openSet.Count > 0 && temp)
            {
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost))
                    {
                        if (openSet[i].hCost < currentNode.hCost)
                            currentNode = openSet[i];
                    }
                }
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
                
                if (currentNode == finishNode)
                {
                    nextNode = (RetracePath(startNode, finishNode))[0];
                    temp = false;
                }

                foreach (Node neighbor in grid.GetNeighbors(currentNode))
                {
                    if (neighbor.walkable && !closedSet.Contains(neighbor))
                    {
                        int newMoveCosttoNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);

                        if (newMoveCosttoNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                        {
                            neighbor.gCost = newMoveCosttoNeighbor;
                            neighbor.hCost = GetDistance(neighbor, finishNode);

                            neighbor.parent = currentNode;

                            if (!openSet.Contains(neighbor))
                            {
                                openSet.Add(neighbor);
                            }
                        }
                    }
                }
            }
            return nextNode;
        }
        
        
        private int GetDistance(Node node1, Node node2)
        {
            int distanceX = Math.Abs(node1.gridX - node2.gridX);
            int distanceY = Math.Abs(node1.gridY - node2.gridY);

            if (distanceX > distanceY)
            {
                return 14 * distanceY + 10 * (distanceX - distanceY);
            }
            else
            {
                return 14 * distanceX + 10 * (distanceY - distanceX);
            }
        }

        private List<Node> RetracePath(Node startNode, Node finishNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = finishNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();

            return path;
        }
    }
}