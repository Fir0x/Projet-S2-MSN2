using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    /*Grid grid;
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
    */
    
    public float speedE;
    public float stoppingDistance;
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speedE + Time.deltaTime);
        }
    }
}