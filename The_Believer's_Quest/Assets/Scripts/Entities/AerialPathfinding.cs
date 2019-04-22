using UnityEngine;
using System;


namespace Entities
{
    public class AerialPathfinding : MonoBehaviour
    {
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
}