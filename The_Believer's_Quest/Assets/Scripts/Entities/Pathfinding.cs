using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid grid;
    public Transform startPosition;
    public Transform finishPosition;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        FindPath(startPosition.position, finishPosition.position);
    }

    private void FindPath(Vector3 startpos, Vector3 finishpos)
    {
        Node startNode = grid.GetPos();
    }
}