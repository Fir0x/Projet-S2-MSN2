using System;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public class EnemiesList : MonoBehaviour
    {
        private GameObject[] allMobs;
        [SerializeField] private GameObject blueSlime;
        [SerializeField] private GameObject earthSentinel;

        public GameObject BlueSlime { get => blueSlime; set => blueSlime = value; }
        public GameObject EarthSentinel { get => earthSentinel; set => earthSentinel = value; }

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