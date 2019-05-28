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
        public void Move(Enemy enemy)
        {
            if (Vector2.Distance(enemy.transform.position, target.position) > stoppingDistance)
            {
                enemy.transform.position =
                    Vector2.MoveTowards(enemy.transform.position, target.position, speedE + Time.deltaTime);
            }
            enemy.gameObject.GetComponent<Attack>().Launcher();
        }
    }
}