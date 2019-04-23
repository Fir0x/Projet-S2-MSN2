using UnityEngine;
using System;

//Maxence
namespace Entities
{
    public class AerialPathfinding : MonoBehaviour
    {
        private float speedE = 0.1f;
        private float stoppingDistance = 2f;
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
}