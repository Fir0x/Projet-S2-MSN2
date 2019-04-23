using System;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public class EnemiesList : MonoBehaviour
    {
        private GameObject[] allMobs;
        [SerializeField] private GameObject BlueSlime;
        [SerializeField] private GameObject EarthSentinel;

        void Start()
        {
            allMobs = new[]
            {
                BlueSlime, EarthSentinel
            };
        }

        public GameObject[] GetAllMobs()
        {
            return allMobs;
        }
    }
}