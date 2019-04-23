using System.Collections.Generic;
using UnityEngine;
//Nicolas L
namespace Entities
{
    public class EnemiesRoom : EnemiesList
    {
        /*private List<GameObject> enemies = new List<GameObject>();
        private int totalWeight = 7;             //pour étage 1 seulement
        
        public void CreateEnemies()
        {
            GameObject enemy;
            while (totalWeight > 0)
            {
                enemy = GetAllMobs()[UnityEngine.Random.Range(0, GetAllMobs().Length)].gameObject;
                enemies.Add(enemy);

                if (totalWeight - enemy.GetComponent<Enemies>().GetWeight() < 0)
                {
                    totalWeight = 0;
                }
                else
                {
                    enemies.Add(enemy);
                    totalWeight -= enemy.GetComponent<Enemies>().GetWeight();
                }
            }
        }

        public List<GameObject> GetEnemiesRoom()
        {
            return enemies;
        }*/
    }
}